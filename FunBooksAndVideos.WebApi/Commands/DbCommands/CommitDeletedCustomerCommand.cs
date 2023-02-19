using MediatR;

namespace FunBooksAndVideos.WebApi.Commands.DbCommands
{
    public record CommitDeletedCustomerCommand(Guid Id) : IRequest<Unit>;
}
