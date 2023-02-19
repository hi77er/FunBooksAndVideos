using MediatR;

namespace FunBooksAndVideos.WebApi.Commands.DbCommands
{
    public record CommitDeletedItemCommand(Guid Id) : IRequest<Unit>;
}
