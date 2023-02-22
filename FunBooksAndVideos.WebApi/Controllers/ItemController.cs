using FunBooksAndVideos.DTOs;
using FunBooksAndVideos.WebApi.Commands;
using FunBooksAndVideos.WebApi.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FunBooksAndVideos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ItemController(IMediator mediator) => _mediator = mediator;
        

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetAll()
        {
            var all = await _mediator.Send(new GetAllItemsQuery());
            return Ok(all);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> Get(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var found = await _mediator.Send(new GetItemByIdQuery(id));
            return Ok(found);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Item item)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _mediator.Send(new CommitItemCommand(item));
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Item item)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _mediator.Send(new CommitUpdatedItemCommand(item));
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _mediator.Send(new CommitDeletedItemCommand(id));
            return Ok();
        }

    }
}