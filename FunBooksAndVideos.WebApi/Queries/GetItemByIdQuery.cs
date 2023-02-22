using FunBooksAndVideos.DTOs;
using MediatR;

namespace FunBooksAndVideos.WebApi.Queries
{
    public record GetItemByIdQuery(Guid Id) : IRequest<Item>;
}
    