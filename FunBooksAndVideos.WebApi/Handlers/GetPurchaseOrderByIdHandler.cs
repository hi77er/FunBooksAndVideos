using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.DAL.Entities;
using FunBooksAndVideos.WebApi.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FunBooksAndVideos.WebApi.Handlers
{
    public class GetPurchaseOrderByIdHandler : IRequestHandler<GetPurchaseOrderByIdQuery, PurchaseOrder>
    {
        private readonly FunDbContext _dbContext;
        public GetPurchaseOrderByIdHandler(FunDbContext dbContext) => _dbContext = dbContext;

        public async Task<PurchaseOrder> Handle(
            GetPurchaseOrderByIdQuery request,
            CancellationToken cancellationToken)
            => await _dbContext
                .PurchaseOrders
                .FirstOrDefaultAsync(x => x.Id.Equals(request.Id), cancellationToken);
    }
}
