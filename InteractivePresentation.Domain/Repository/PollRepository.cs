using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InteractivePresentation.Domain.Entity;
using InteractivePresentation.Domain.Repository.Abstract;

namespace InteractivePresentation.Domain.Repository
{
    public class PollRepository(IGenericRepositoryAsync<Poll, Guid> repository) : IPollRepository
    {
        public async Task<IEnumerable<Poll>> GetPollsAsync(Guid presentationId)
        {
            return (await repository.GetAllAsync()).Where(p => p.PresentationId == presentationId).ToList();
        }

        public async Task<Poll> GetCurrentPollAsync(Guid presentationId)
        {
            return (await repository.GetAllAsync()).FirstOrDefault(p => p.PresentationId == presentationId);
        }

        public async Task CreatePollAsync(Poll poll)
        {
            await repository.CreateAsync(poll);
        }

        public async Task UpdatePollAsync(Poll poll)
        {
            await repository.UpdateAsync(poll);
        }

        public async Task<Poll> GetPollByIdAsync(Guid pollId)
        {
           return await repository.GetByIdAsync(pollId);
        }
    }
}