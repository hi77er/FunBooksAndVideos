using FunBooksAndVideos.DAL.Entities;
using MediatR;

namespace FunBooksAndVideos.WebApi.Commands.DbCommands
{
    public record CommitUpdatedCustomerCommand(Customer Customer) : IRequest<Unit>;
}
