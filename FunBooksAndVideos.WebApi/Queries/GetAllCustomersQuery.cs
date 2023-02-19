using FunBooksAndVideos.DAL.Entities;
using MediatR;

namespace FunBooksAndVideos.WebApi.Queries
{
    public record GetAllCustomersQuery() : IRequest<IEnumerable<Customer>>;
}
