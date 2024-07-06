using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InteractivePresentation.Domain.Entity;
using InteractivePresentation.Domain.Model;
using InteractivePresentation.Domain.Repository.Abstract;
using InteractivePresentation.Domain.Service.Abstract;

namespace InteractivePresentation.Domain.Service
{
    public class PollService(IPollRepository pollRepository, IVoteRepository voteRepository)
        : IPollService
    {
        public async Task<PollResponse> GetCurrentPollAsync(Guid presentationId)
        {
            var currentPoll = await pollRepository.GetCurrentPollAsync(presentationId);

            return Map(currentPoll);
        }

        private static PollResponse Map(Poll poll)
        {
            if (poll == null)
            {
                return null;
            }

            return new PollResponse
            {
                Id = poll.Id,
                PresentationId = poll.PresentationId,
                Question = poll.Question,
                Options = poll.Options?.Select(v => new OptionResponse
                {
                    Key = v.Key,
                    Value = v.Value
                }).ToList()
            };
        }

        public async Task<PollResponse> SetCurrentPollAsync(Guid presentationId, Poll poll)
        {
            await pollRepository.SetCurrentPollAsync(presentationId, poll);

            return Map(poll);
        }

        public async Task CreateVoteAsync(Guid presentationId, Guid pollId, VoteRequest vote)
        {
            var currentPoll = await pollRepository.GetCurrentPollAsync(presentationId);
            if (currentPoll == null || currentPoll.Id != pollId)
            {
                throw new Exception("Invalid poll");
            }
            await voteRepository.CreateVoteAsync(new Vote
            {
                ClientId = vote.ClientId,
                Key = vote.Key,
                PollId = vote.PollId,
            });
        }

        public async Task<IEnumerable<VoterResponse>> GetVotesAsync(Guid presentationId, Guid pollId)
        {
            var votes = await voteRepository.GetVotesAsync(pollId);

            return votes.Select(vote => new VoterResponse
            {
                ClientId = vote.ClientId,
                Key = vote.Key
            });
        }
    }
}
