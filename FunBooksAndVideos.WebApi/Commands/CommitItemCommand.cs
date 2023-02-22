using FunBooksAndVideos.DTOs;
using MediatR;

namespace FunBooksAndVideos.WebApi.Commands
{
    public record CommitItemCommand(Item Item) : IRequest<Unit>;
}
