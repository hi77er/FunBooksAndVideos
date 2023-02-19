using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.DAL.Entities;
using FunBooksAndVideos.WebApi.Handlers.Base;
using FunBooksAndVideos.WebApi.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FunBooksAndVideos.WebApi.Handlers.DbHandlers
{
    public class GetItemByIdHandler
        : DbHandler, IRequestHandler<GetItemByIdQuery, Item>
    {
        public GetItemByIdHandler(FunDbContext dbContext)
            : base(dbContext) { }

        public async Task<Item> Handle(
            GetItemByIdQuery request,
            CancellationToken cancellationToken)
            => await _dbContext
                .Items
                .FirstOrDefaultAsync(x => x.Id.Equals(request.Id), cancellationToken);
    }
}
