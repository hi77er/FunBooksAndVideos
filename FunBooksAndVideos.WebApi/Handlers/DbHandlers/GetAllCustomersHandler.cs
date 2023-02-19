using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.DAL.Entities;
using FunBooksAndVideos.WebApi.Handlers.Base;
using FunBooksAndVideos.WebApi.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FunBooksAndVideos.WebApi.Handlers.DbHandlers
{
    public class GetAllCustomersHandler
        : DbHandler, IRequestHandler<GetAllCustomersQuery, IEnumerable<Customer>>
    {
        public GetAllCustomersHandler(FunDbContext dbContext)
            : base(dbContext) { }

        public async Task<IEnumerable<Customer>> Handle(
            GetAllCustomersQuery request,
            CancellationToken cancellationToken)
            => await _dbContext.Customers.ToListAsync();

    }
}
