using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.DAL.Entities;
using FunBooksAndVideos.WebApi.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FunBooksAndVideos.WebApi.Handlers
{
    public class GetAllCustomersHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<Customer>>
    {
        private readonly FunDbContext _dbContext;
        public GetAllCustomersHandler(FunDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<Customer>> Handle(
            GetAllCustomersQuery request,
            CancellationToken cancellationToken)
            => await _dbContext.Customers.ToListAsync();

    }
}
