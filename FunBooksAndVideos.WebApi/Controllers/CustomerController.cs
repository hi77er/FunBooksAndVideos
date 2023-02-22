using FunBooksAndVideos.DTOs;
using FunBooksAndVideos.WebApi.Commands;
using FunBooksAndVideos.WebApi.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FunBooksAndVideos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator) => _mediator = mediator;
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAll()
        {
            var all = await _mediator.Send(new GetAllCustomersQuery());
            return Ok(all);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Get(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var found = await _mediator.Send(new GetCustomerByIdQuery(id));
            return Ok(found);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Customer customer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _mediator.Send(new CommitCustomerCommand(customer));
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Customer customer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _mediator.Send(new CommitUpdatedCustomerCommand(customer));
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _mediator.Send(new CommitDeletedCustomerCommand(id));
            return Ok();
        }

    }
}