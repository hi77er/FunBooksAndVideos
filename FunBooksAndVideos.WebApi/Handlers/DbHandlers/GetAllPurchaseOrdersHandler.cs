using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.DAL.Entities;
using FunBooksAndVideos.WebApi.Handlers.Base;
using FunBooksAndVideos.WebApi.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FunBooksAndVideos.WebApi.Handlers.DbHandlers
{
    public class GetAllPurchaseOrdersHandler
        : DbHandler, IRequestHandler<GetAllPurchaseOrdersQuery, IEnumerable<PurchaseOrder>>
    {
        public GetAllPurchaseOrdersHandler(FunDbContext dbContext)
            : base(dbContext) { }

        public async Task<IEnumerable<PurchaseOrder>> Handle(
            GetAllPurchaseOrdersQuery request,
            CancellationToken cancellationToken)
            => await _dbContext.PurchaseOrders.ToListAsync();

    }
}
