using FunBooksAndVideos.DAL.Context;
using MediatR;

namespace FunBooksAndVideos.WebApi.Handlers.Base
{
    public interface IDbRequestHandler<TRequest> 
        : IRequestHandler<TRequest, FunDbContext> 
        where TRequest : IRequest<FunDbContext>
    {
    }
}
