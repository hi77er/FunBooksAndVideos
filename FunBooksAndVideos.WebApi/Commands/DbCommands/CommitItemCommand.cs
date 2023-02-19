using FunBooksAndVideos.DAL.Entities;
using MediatR;

namespace FunBooksAndVideos.WebApi.Commands.DbCommands
{
    public record CommitItemCommand(Item Item) : IRequest<Unit>;
}
