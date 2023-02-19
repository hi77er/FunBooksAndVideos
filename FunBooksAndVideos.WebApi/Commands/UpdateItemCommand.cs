using FunBooksAndVideos.DAL.Entities;
using MediatR;

namespace FunBooksAndVideos.WebApi.Commands
{
    public record UpdateItemCommand(Item Item) : IRequest<Unit>;
}
