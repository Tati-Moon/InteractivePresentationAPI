using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InteractivePresentation.Client.Service.Abstract;
using InteractivePresentation.Client.Models;
using InteractivePresentation.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PresentationsController(IPresentationClientService clientService) : ControllerBase
    {
        [HttpGet("{presentationId}")]
        public async Task<IActionResult> Get([Required] Guid presentation_id)
        {
            if (presentation_id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(presentation_id));
            }
            var response = await clientService.GetAsync(presentation_id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PresentationRequest presentation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await clientService.PostAsync(presentation);
            //return Ok(response);
            return Created("/presentations/" + response.PresentationId, new { presentation_id = response.PresentationId });
        }
    }
}
