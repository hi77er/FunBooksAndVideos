using MediatR;

namespace FunBooksAndVideos.WebApi.Commands
{
    public record DeleteItemCommand(Guid Id) : IRequest<Unit>;
}
