using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.WebApi.Commands.DbCommands;
using FunBooksAndVideos.WebApi.Handlers.Base;

namespace FunBooksAndVideos.WebApi.Handlers.DbHandlers
{
    public class AddPurchaseOrderHandler
        : DbHandler, IDbRequestHandler<AddPurchaseOrderCommand>
    {
        public AddPurchaseOrderHandler(FunDbContext dbContext)
            : base(dbContext) { }

        public async Task<FunDbContext> Handle(AddPurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            _dbContext.PurchaseOrders.Add(request.PurchaseOrder);
            return _dbContext;
        }
    }
}
