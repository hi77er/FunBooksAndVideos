using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.WebApi.Commands.DbCommands;
using FunBooksAndVideos.WebApi.Handlers.Base;
using MediatR;

namespace FunBooksAndVideos.WebApi.Handlers.DbHandlers
{
    public class CommitItemHandler
        : DbHandler, IRequestHandler<CommitItemCommand, Unit>
    {
        public CommitItemHandler(FunDbContext dbContext)
        : base(dbContext) { }

        public async Task<Unit> Handle(CommitItemCommand request, CancellationToken cancellationToken)
        {
            _dbContext.Items.Add(request.Item);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
