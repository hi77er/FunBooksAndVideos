using FunBooksAndVideos.DTOs;
using MediatR;

namespace FunBooksAndVideos.WebApi.Commands
{
    public record CommitUpdatedCustomerCommand(Customer Customer) : IRequest<Unit>;
}
