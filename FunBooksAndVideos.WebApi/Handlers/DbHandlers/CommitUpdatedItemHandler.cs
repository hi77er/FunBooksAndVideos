using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.WebApi.Commands.DbCommands;
using FunBooksAndVideos.WebApi.Handlers.Base;
using MediatR;

namespace FunBooksAndVideos.WebApi.Handlers.DbHandlers
{
    public class CommitUpdatedItemHandler
        : DbHandler, IRequestHandler<CommitUpdatedItemCommand, Unit>
    {
        public CommitUpdatedItemHandler(FunDbContext dbContext)
            : base(dbContext) { }

        public async Task<Unit> Handle(CommitUpdatedItemCommand request, CancellationToken cancellationToken)
        {
            _dbContext.Items.Update(request.Item);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
