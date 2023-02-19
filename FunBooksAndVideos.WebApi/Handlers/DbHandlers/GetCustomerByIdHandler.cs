using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.DAL.Entities;
using FunBooksAndVideos.WebApi.Handlers.Base;
using FunBooksAndVideos.WebApi.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FunBooksAndVideos.WebApi.Handlers.DbHandlers
{
    public class GetCustomerByIdHandler
        : DbHandler, IRequestHandler<GetCustomerByIdQuery, Customer>
    {
        public GetCustomerByIdHandler(FunDbContext dbContext)
            : base(dbContext) { }

        public async Task<Customer> Handle(
            GetCustomerByIdQuery request,
            CancellationToken cancellationToken)
            => await _dbContext
                .Customers
                .FirstOrDefaultAsync(x => x.Id.Equals(request.Id), cancellationToken);
    }
}
