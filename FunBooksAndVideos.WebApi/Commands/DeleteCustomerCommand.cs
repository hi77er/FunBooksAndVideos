using MediatR;

namespace FunBooksAndVideos.WebApi.Commands
{
    public record DeleteCustomerCommand(Guid Id) : IRequest<Unit>;
}
