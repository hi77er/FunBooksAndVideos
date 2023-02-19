using FunBooksAndVideos.DAL.Entities;
using MediatR;

namespace FunBooksAndVideos.WebApi.Commands
{
    public record UpdatePurchaseOrderCommand(PurchaseOrder PurchaseOrder) : IRequest<Unit>;
}
