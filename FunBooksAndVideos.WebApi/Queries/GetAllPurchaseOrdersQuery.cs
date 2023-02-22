using FunBooksAndVideos.DTOs;
using MediatR;

namespace FunBooksAndVideos.WebApi.Queries
{
    public record GetAllPurchaseOrdersQuery() : IRequest<IEnumerable<PurchaseOrder>>;
}
