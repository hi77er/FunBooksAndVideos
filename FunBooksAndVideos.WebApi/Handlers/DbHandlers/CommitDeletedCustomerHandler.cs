using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.WebApi.Commands.DbCommands;
using FunBooksAndVideos.WebApi.Handlers.Base;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FunBooksAndVideos.WebApi.Handlers.DbHandlers
{
    public class CommitDeletedCustomerHandler
        : DbHandler, IRequestHandler<CommitDeletedCustomerCommand, Unit>
    {
        public CommitDeletedCustomerHandler(FunDbContext dbContext)
            : base(dbContext) { }

        public async Task<Unit> Handle(CommitDeletedCustomerCommand request, CancellationToken cancellationToken)
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
