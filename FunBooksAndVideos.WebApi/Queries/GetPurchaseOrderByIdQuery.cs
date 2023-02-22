using FunBooksAndVideos.DTOs;
using MediatR;

namespace FunBooksAndVideos.WebApi.Queries
{
    public record GetPurchaseOrderByIdQuery(Guid Id) : IRequest<PurchaseOrder>;
}
