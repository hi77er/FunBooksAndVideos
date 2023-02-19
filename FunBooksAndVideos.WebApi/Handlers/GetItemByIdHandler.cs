using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.DAL.Entities;
using FunBooksAndVideos.WebApi.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FunBooksAndVideos.WebApi.Handlers
{
    public class GetItemByIdHandler : IRequestHandler<GetItemByIdQuery, Item>
    {
        private readonly FunDbContext _dbContext;
        public GetItemByIdHandler(FunDbContext dbContext) => _dbContext = dbContext;

        public async Task<Item> Handle(
            GetItemByIdQuery request,
            CancellationToken cancellationToken)
            => await _dbContext
                .Items
                .FirstOrDefaultAsync(x => x.Id.Equals(request.Id), cancellationToken);
    }
}
