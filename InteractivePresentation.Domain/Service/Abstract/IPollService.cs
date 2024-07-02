using InteractivePresentation.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InteractivePresentation.Domain.Service.Abstract
{
    public interface IPollService
    {
        Task<Poll> GetCurrentPollAsync(Guid presentationId);
        Task<Poll> SetCurrentPollAsync(Guid presentationId, Poll poll);
        Task CreateVoteAsync(Guid presentationId, Guid pollId, Vote vote);
        Task<IEnumerable<Vote>> GetVotesAsync(Guid presentationId, Guid pollId);
    }
}
