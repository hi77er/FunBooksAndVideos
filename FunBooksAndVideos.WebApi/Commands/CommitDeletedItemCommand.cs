using MediatR;

namespace FunBooksAndVideos.WebApi.Commands
{
    public record CommitDeletedItemCommand(Guid Id) : IRequest<Unit>;
}
