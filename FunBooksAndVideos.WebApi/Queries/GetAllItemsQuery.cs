using FunBooksAndVideos.DTOs;
using MediatR;

namespace FunBooksAndVideos.WebApi.Queries
{
    public record GetAllItemsQuery() : IRequest<IEnumerable<Item>>;
}
