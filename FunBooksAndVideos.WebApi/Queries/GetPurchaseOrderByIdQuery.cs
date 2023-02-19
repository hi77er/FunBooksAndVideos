using FunBooksAndVideos.DAL.Entities;
using MediatR;

namespace FunBooksAndVideos.WebApi.Queries
{
    public record GetPurchaseOrderByIdQuery(Guid Id) : IRequest<PurchaseOrder>;
}
