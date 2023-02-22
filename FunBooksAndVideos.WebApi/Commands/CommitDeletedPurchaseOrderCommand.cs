using MediatR;

namespace FunBooksAndVideos.WebApi.Commands
{
    public record CommitDeletedPurchaseOrderCommand(Guid Id) : IRequest<Unit>;
}
