using FunBooksAndVideos.DAL.Entities;
using MediatR;

namespace FunBooksAndVideos.WebApi.Commands.DbCommands
{
    public record CommitUpdatedItemCommand(Item Item) : IRequest<Unit>;
}
