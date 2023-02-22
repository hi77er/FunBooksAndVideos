using FunBooksAndVideos.DTOs;
using MediatR;

namespace FunBooksAndVideos.WebApi.Queries
{
    public record GetAllCustomersQuery() : IRequest<IEnumerable<Customer>>;
}
