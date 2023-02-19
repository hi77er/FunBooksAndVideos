using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.DAL.Entities;
using FunBooksAndVideos.WebApi.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FunBooksAndVideos.WebApi.Handlers
{
    public class GetAllPurchaseOrdersHandler : IRequestHandler<GetAllPurchaseOrdersQuery, IEnumerable<PurchaseOrder>>
    {
        private readonly FunDbContext _dbContext;
        public GetAllPurchaseOrdersHandler(FunDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<PurchaseOrder>> Handle(
            GetAllPurchaseOrdersQuery request,
            CancellationToken cancellationToken)
            => await _dbContext.PurchaseOrders.ToListAsync();

    }
}
