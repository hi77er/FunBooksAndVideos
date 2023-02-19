using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.WebApi.Commands.DbCommands;
using FunBooksAndVideos.WebApi.Handlers.Base;
using MediatR;

namespace FunBooksAndVideos.WebApi.Handlers.DbHandlers
{
    public class CommitCustomerHandler
        : DbHandler, IRequestHandler<CommitCustomerCommand, Unit>
    {
        public CommitCustomerHandler(FunDbContext dbContext)
            : base(dbContext) { }

        public async Task<Unit> Handle(CommitCustomerCommand request, CancellationToken cancellationToken)
        {
            _dbContext.Customers.Add(request.Customer);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
