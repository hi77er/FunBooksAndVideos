using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.DAL.Entities;
using FunBooksAndVideos.WebApi.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FunBooksAndVideos.WebApi.Handlers
{
    public class GetAllItemsHandler : IRequestHandler<GetAllItemsQuery, IEnumerable<Item>>
    {
        private readonly FunDbContext _dbContext;
        public GetAllItemsHandler(FunDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<Item>> Handle(
            GetAllItemsQuery request,
            CancellationToken cancellationToken)
            => await _dbContext.Items.ToListAsync();

    }
}
