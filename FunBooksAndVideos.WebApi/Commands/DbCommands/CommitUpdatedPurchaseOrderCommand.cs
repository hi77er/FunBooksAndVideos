using FunBooksAndVideos.DAL.Entities;
using MediatR;

namespace FunBooksAndVideos.WebApi.Commands.DbCommands
{
    public record CommitUpdatedPurchaseOrderCommand(PurchaseOrder PurchaseOrder) : IRequest<Unit>;
}
