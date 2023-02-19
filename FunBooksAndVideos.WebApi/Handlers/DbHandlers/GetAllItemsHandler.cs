using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.DAL.Entities;
using FunBooksAndVideos.WebApi.Handlers.Base;
using FunBooksAndVideos.WebApi.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FunBooksAndVideos.WebApi.Handlers.DbHandlers
{
    public class GetAllItemsHandler
        : DbHandler, IRequestHandler<GetAllItemsQuery, IEnumerable<Item>>
    {
        public GetAllItemsHandler(FunDbContext dbContext)
            : base(dbContext) { }

        public async Task<IEnumerable<Item>> Handle(
            GetAllItemsQuery request,
            CancellationToken cancellationToken)
            => await _dbContext.Items.ToListAsync();

    }
}
