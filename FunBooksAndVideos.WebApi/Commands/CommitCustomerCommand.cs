using FunBooksAndVideos.DTOs;
using MediatR;

namespace FunBooksAndVideos.WebApi.Commands
{
    public record CommitCustomerCommand(Customer Customer) : IRequest<Unit>;
}
