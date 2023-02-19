using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.DAL.Entities;
using FunBooksAndVideos.WebApi.Handlers.Base;
using FunBooksAndVideos.WebApi.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FunBooksAndVideos.WebApi.Handlers.DbHandlers
{
    public class GetPurchaseOrderByIdHandler
        : DbHandler, IRequestHandler<GetPurchaseOrderByIdQuery, PurchaseOrder>
    {
        public GetPurchaseOrderByIdHandler(FunDbContext dbContext)
            : base(dbContext) { }

        public async Task<PurchaseOrder> Handle(
            GetPurchaseOrderByIdQuery request,
            CancellationToken cancellationToken)
            => await _dbContext
                .PurchaseOrders
                .FirstOrDefaultAsync(x => x.Id.Equals(request.Id), cancellationToken);
    }
}
