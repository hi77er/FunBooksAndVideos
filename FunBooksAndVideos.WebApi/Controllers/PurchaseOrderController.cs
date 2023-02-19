using FunBooksAndVideos.DAL.Entities;
using FunBooksAndVideos.WebApi.Commands.DbCommands;
using FunBooksAndVideos.WebApi.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FunBooksAndVideos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PurchaseOrderController(IMediator mediator) => _mediator = mediator;


        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseOrder>>> GetAll()
        {
            var all = await _mediator.Send(new GetAllPurchaseOrdersQuery());
            return Ok(all);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseOrder>> Get(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var found = await _mediator.Send(new GetPurchaseOrderByIdQuery(id));
            return Ok(found);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PurchaseOrder purchaseOrder)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            await _mediator.Send(new AddPurchaseOrderCommand(purchaseOrder));
            await _mediator.Send(new AddShippingSlipCommand(purchaseOrder));
            var ctx = await _mediator.Send(new AddMembershipsCommand(purchaseOrder));
            await ctx.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PurchaseOrder purchaseOrder)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _mediator.Send(new CommitUpdatedPurchaseOrderCommand(purchaseOrder));
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _mediator.Send(new CommitDeletedPurchaseOrderCommand(id));
            return Ok();
        }

    }
}