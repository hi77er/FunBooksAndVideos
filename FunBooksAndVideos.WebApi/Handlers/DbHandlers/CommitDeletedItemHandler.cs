using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.WebApi.Commands.DbCommands;
using FunBooksAndVideos.WebApi.Handlers.Base;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FunBooksAndVideos.WebApi.Handlers.DbHandlers
{
    public class CommitDeletedItemHandler
        : DbHandler, IRequestHandler<CommitDeletedItemCommand, Unit>
    {
        public CommitDeletedItemHandler(FunDbContext dbContext)
            : base(dbContext) { }

        public async Task<Unit> Handle(CommitDeletedItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _dbContext
                .Items
                .FirstOrDefaultAsync(x => x.Id.Equals(request.Id));

            _dbContext.Items.Remove(item);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
