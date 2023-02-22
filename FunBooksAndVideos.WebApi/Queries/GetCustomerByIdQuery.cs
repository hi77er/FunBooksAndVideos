using FunBooksAndVideos.DTOs;
using MediatR;

namespace FunBooksAndVideos.WebApi.Queries
{
    public record GetCustomerByIdQuery(Guid Id) : IRequest<Customer>;
}
