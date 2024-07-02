using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InteractivePresentation.Domain.Entity;
using InteractivePresentation.Domain.Repository.Abstract;
using InteractivePresentation.Domain.Service.Abstract;

namespace InteractivePresentation.Domain.Service
{
    public class PollService(IPollRepository pollRepository, IVoteRepository voteRepository)
        : IPollService
    {
        public async Task<Poll> GetCurrentPollAsync(Guid presentationId)
        {
            return await pollRepository.GetCurrentPollAsync(presentationId);
        }

        public async Task<Poll> SetCurrentPollAsync(Guid presentationId, Poll poll)
        {
            var currentPoll = await pollRepository.GetCurrentPollAsync(presentationId);
            if (currentPoll != null)
            {
                await pollRepository.UpdatePollAsync(currentPoll);
            }

            await pollRepository.UpdatePollAsync(poll);

            return poll;
        }

        public async Task CreateVoteAsync(Guid presentationId, Guid pollId, Vote vote)
        {
            var currentPoll = await pollRepository.GetCurrentPollAsync(presentationId);
            if (currentPoll == null || currentPoll.Id != pollId)
            {
                throw new Exception("Invalid poll");
            }

            await voteRepository.CreateVoteAsync(vote);
        }

        public async Task<IEnumerable<Vote>> GetVotesAsync(Guid presentationId, Guid pollId)
        {
            return await voteRepository.GetVotesAsync(pollId);
        }
    }
}
