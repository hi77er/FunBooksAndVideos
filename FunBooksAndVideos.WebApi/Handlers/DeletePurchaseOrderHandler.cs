
using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.WebApi.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FunBooksAndVideos.WebApi.Handlers
{
    public class DeletePurchaseOrderHandler : IRequestHandler<DeletePurchaseOrderCommand, Unit>
    {
        private readonly FunDbContext _dbContext;
        public DeletePurchaseOrderHandler(FunDbContext dbContext) => _dbContext = dbContext;
        
        public async Task<Unit> Handle(DeletePurchaseOrderCommand request, CancellationToken cancellationToken)
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
