using MediatR;

namespace FunBooksAndVideos.WebApi.Commands.DbCommands
{
    public record CommitDeletedPurchaseOrderCommand(Guid Id) : IRequest<Unit>;
}
