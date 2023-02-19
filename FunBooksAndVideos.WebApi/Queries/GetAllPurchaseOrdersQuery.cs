using FunBooksAndVideos.DAL.Entities;
using MediatR;

namespace FunBooksAndVideos.WebApi.Queries
{
    public record GetAllPurchaseOrdersQuery() : IRequest<IEnumerable<PurchaseOrder>>;
}
