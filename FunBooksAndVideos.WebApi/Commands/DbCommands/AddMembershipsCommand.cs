using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.DAL.Entities;
using MediatR;

namespace FunBooksAndVideos.WebApi.Commands.DbCommands
{
    public record AddMembershipsCommand(PurchaseOrder PurchaseOrder) 
        : IRequest<FunDbContext>;
}
