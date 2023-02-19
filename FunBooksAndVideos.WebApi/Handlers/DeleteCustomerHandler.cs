
using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.WebApi.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FunBooksAndVideos.WebApi.Handlers
{
    public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, Unit>
    {
        private readonly FunDbContext _dbContext;
        public DeleteCustomerHandler(FunDbContext dbContext) => _dbContext = dbContext;
        
        public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _dbContext
                .Customers
                .FirstOrDefaultAsync(x => x.Id.Equals(request.Id));
            
            _dbContext.Customers.Remove(customer);
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}
