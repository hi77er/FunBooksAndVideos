using FunBooksAndVideos.DTOs;
using MediatR;

namespace FunBooksAndVideos.WebApi.Commands
{
    public record CommitUpdatedPurchaseOrderCommand(PurchaseOrder PurchaseOrder) : IRequest<Unit>;
}
