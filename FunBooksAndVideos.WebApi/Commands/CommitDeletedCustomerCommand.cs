using MediatR;

namespace FunBooksAndVideos.WebApi.Commands
{
    public record CommitDeletedCustomerCommand(Guid Id) : IRequest<Unit>;
}
