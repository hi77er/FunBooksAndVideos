using FunBooksAndVideos.DAL.Entities;
using MediatR;

namespace FunBooksAndVideos.WebApi.Commands
{
    public record UpdateCustomerCommand(Customer Customer) : IRequest<Unit>;
}
