using FunBooksAndVideos.DAL.Entities;
using MediatR;

namespace FunBooksAndVideos.WebApi.Commands
{
    public record AddItemCommand(Item Item) : IRequest<Unit>;
}
