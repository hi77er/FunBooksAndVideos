using FunBooksAndVideos.DAL.Entities;
using MediatR;

namespace FunBooksAndVideos.WebApi.Commands.DbCommands
{
    public record CommitCustomerCommand(Customer Customer) : IRequest<Unit>;
}
