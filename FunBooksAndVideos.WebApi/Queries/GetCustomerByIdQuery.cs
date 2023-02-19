using FunBooksAndVideos.DAL.Entities;
using MediatR;

namespace FunBooksAndVideos.WebApi.Queries
{
    public record GetCustomerByIdQuery(Guid Id) : IRequest<Customer>;
}
