using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.WebApi.Commands.DbCommands;
using FunBooksAndVideos.WebApi.Handlers.Base;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FunBooksAndVideos.WebApi.Handlers.DbHandlers
{
    public class CommitDeletedPurchaseOrderHandler
        : DbHandler, IRequestHandler<CommitDeletedPurchaseOrderCommand, Unit>
    {
        public CommitDeletedPurchaseOrderHandler(FunDbContext dbContext)
            : base(dbContext) { }

        public async Task<Unit> Handle(CommitDeletedPurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            var purchaseOrder = await _dbContext
                .PurchaseOrders
                .FirstOrDefaultAsync(x => x.Id.Equals(request.Id));

            _dbContext.PurchaseOrders.Remove(purchaseOrder);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
