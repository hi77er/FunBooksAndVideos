using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.WebApi.Commands.DbCommands;
using FunBooksAndVideos.WebApi.Handlers.Base;
using MediatR;

namespace FunBooksAndVideos.WebApi.Handlers.DbHandlers
{
    public class CommitUpdatedPurchaseOrderHandler
        : DbHandler, IRequestHandler<CommitUpdatedPurchaseOrderCommand, Unit>
    {
        public CommitUpdatedPurchaseOrderHandler(FunDbContext dbContext)
            : base(dbContext) { }

        public async Task<Unit> Handle(CommitUpdatedPurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            _dbContext.PurchaseOrders.Update(request.PurchaseOrder);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
