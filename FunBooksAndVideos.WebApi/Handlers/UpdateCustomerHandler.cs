
using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.WebApi.Commands;
using MediatR;

namespace FunBooksAndVideos.WebApi.Handlers
{
    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, Unit>
    {
        private readonly FunDbContext _dbContext;
        public UpdateCustomerHandler(FunDbContext dbContext) => _dbContext = dbContext;
        
        public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            _dbContext.Customers.Update(request.Customer);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
