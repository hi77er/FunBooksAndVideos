using MediatR;

namespace FunBooksAndVideos.WebApi.Commands
{
    public record DeletePurchaseOrderCommand(Guid Id) : IRequest<Unit>;
}
