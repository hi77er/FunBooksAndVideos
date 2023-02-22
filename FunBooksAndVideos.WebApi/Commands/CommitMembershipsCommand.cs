using FunBooksAndVideos.DTOs;
using MediatR;

namespace FunBooksAndVideos.WebApi.Commands
{
    public record CommitPurchaseOrderCommand(PurchaseOrder PurchaseOrder)
        : IRequest<Unit>;
}
