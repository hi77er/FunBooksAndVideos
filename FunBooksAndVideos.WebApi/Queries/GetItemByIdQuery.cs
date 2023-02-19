using FunBooksAndVideos.DAL.Entities;
using MediatR;

namespace FunBooksAndVideos.WebApi.Queries
{
    public record GetItemByIdQuery(Guid Id) : IRequest<Item>;
}
