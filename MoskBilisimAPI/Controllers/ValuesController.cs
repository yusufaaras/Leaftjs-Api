using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoskBilisimAPI.CQRS.Commands;
using MoskBilisimAPI.CQRS.Handlers;
using MoskBilisimAPI.CQRS.Queries;

namespace MoskBilisimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ValuesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> SaveMapData([FromBody] CreateShapeCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(new { id });
        }

        [HttpGet]
        public async Task<IActionResult> GetMapData([FromServices] IMediator mediator)
        {
            var query = new GetAllMapDataQuery();
            var mapData = await mediator.Send(query);

            if (mapData == null || mapData.Count == 0)
            {
                return NotFound("Hiç veri bulunamadı.");
            }

            return Ok(mapData);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMapDataById(int id)
        {
            var mapData = await _mediator.Send(new GetShapeByIDQuery(id));
            if (mapData == null)
            {
                return NotFound();
            }
            return Ok(mapData);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveShape(int id)
        {
            var command = new RemoveShapeCommand(id); 
            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound("Silinmek istenen veri bulunamadı.");
            }

            return NoContent(); 
        }
    }
}
