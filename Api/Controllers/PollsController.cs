using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;
using InteractivePresentation.Domain.Entity;
using InteractivePresentation.Domain.Service.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/presentations/{presentation_id}/polls")]
    [ApiController]
    public class PollsController(IPollService pollService) : ControllerBase
    {
        [HttpGet("current")]
        public async Task<ActionResult<Poll>> GetCurrentPoll([FromRoute, Required] Guid presentation_id)
        {
            if (presentation_id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(presentation_id));
            }
            var poll = await pollService.GetCurrentPollAsync(presentation_id);
            if (poll == null)
            {
                return NotFound();
            }
            return Ok(poll);
        }

        [HttpPut("current")]
        public async Task<ActionResult<Poll>> SetCurrentPoll([FromRoute, Required] Guid presentation_id, Poll poll)
        {
            if (poll == null)
            {
                throw new ArgumentNullException(nameof(poll));
            }
            if (presentation_id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(presentation_id));
            }
            try
            {
                var updatedPoll = await pollService.SetCurrentPollAsync(presentation_id, poll);
                if (updatedPoll == null)
                {
                    return NotFound();
                }
                return Ok(updatedPoll);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("current/votes")]
        public async Task<IActionResult> CreateVote([FromRoute, Required] Guid presentation_id, [FromBody] Vote vote)
        {
            if (vote == null)
            {
                throw new ArgumentNullException(nameof(vote));
            }
            if (presentation_id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(presentation_id));
            }
            try
            {
                await pollService.CreateVoteAsync(presentation_id, vote.PollId, vote);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{pollId}/votes")]
        public async Task<ActionResult<IEnumerable<Vote>>> GetVotes([FromRoute, Required] Guid presentation_id, [FromRoute, Required] Guid pollId)
        {
            if (presentation_id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(presentation_id));
            }
            var votes = await pollService.GetVotesAsync(presentation_id, pollId);
            return Ok(votes);
        }
    }
}