
using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.WebApi.Commands;
using MediatR;

namespace FunBooksAndVideos.WebApi.Handlers
{
    public class UpdatePurchaseOrderHandler : IRequestHandler<UpdatePurchaseOrderCommand, Unit>
    {
        private readonly FunDbContext _dbContext;
        public UpdatePurchaseOrderHandler(FunDbContext dbContext) => _dbContext = dbContext;
        
        public async Task<Unit> Handle(UpdatePurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            _dbContext.PurchaseOrders.Update(request.PurchaseOrder);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
