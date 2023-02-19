using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.DAL.Entities;
using FunBooksAndVideos.WebApi.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FunBooksAndVideos.WebApi.Handlers
{
    public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdQuery, Customer>
    {
        private readonly FunDbContext _dbContext;
        public GetCustomerByIdHandler(FunDbContext dbContext) => _dbContext = dbContext;

        public async Task<Customer> Handle(
            GetCustomerByIdQuery request,
            CancellationToken cancellationToken)
            => await _dbContext
                .Customers
                .FirstOrDefaultAsync(x => x.Id.Equals(request.Id), cancellationToken);
    }
}
