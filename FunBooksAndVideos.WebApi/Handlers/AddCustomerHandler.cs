
using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.WebApi.Commands;
using MediatR;

namespace FunBooksAndVideos.WebApi.Handlers
{
    public class AddCustomerHandler : IRequestHandler<AddCustomerCommand, Unit>
    {
        private readonly FunDbContext _dbContext;
        public AddCustomerHandler(FunDbContext dbContext) => _dbContext = dbContext;
        
        public async Task<Unit> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
        {
            _dbContext.Customers.Add(request.Customer);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
