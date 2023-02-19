using FunBooksAndVideos.DAL.Entities;
using MediatR;

namespace FunBooksAndVideos.WebApi.Commands
{
    public record AddCustomerCommand(Customer Customer) : IRequest<Unit>;
}
