using FunBooksAndVideos.DAL.Entities;
using MediatR;

namespace FunBooksAndVideos.WebApi.Queries
{
    public record GetAllItemsQuery() : IRequest<IEnumerable<Item>>;
}
