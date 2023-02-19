using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.WebApi.Commands.DbCommands;
using FunBooksAndVideos.WebApi.Handlers.Base;
using MediatR;

namespace FunBooksAndVideos.WebApi.Handlers.DbHandlers
{
    public class CommitUpdatedCustomerHandler
        : DbHandler, IRequestHandler<CommitUpdatedCustomerCommand, Unit>
    {
        public CommitUpdatedCustomerHandler(FunDbContext dbContext)
            : base(dbContext) { }

        public async Task<Unit> Handle(CommitUpdatedCustomerCommand request, CancellationToken cancellationToken)
        {
            _dbContext.Customers.Update(request.Customer);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
