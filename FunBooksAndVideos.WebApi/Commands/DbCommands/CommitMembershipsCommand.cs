using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.DAL.Entities;
using MediatR;

namespace FunBooksAndVideos.WebApi.Commands.DbCommands
{
    public record CommitPurchaseOrderCommand(PurchaseOrder PurchaseOrder) 
        : IRequest<Unit>;
}
