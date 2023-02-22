using AutoMapper;
using FunBooksAndVideos.DAL.Entities;
using FunBooksAndVideos.Repository.Facade;
using FunBooksAndVideos.WebApi.Commands;
using FunBooksAndVideos.WebApi.Handlers.Base;
using MediatR;

namespace FunBooksAndVideos.WebApi.Handlers
{
    public class CommitCustomerHandler
        : BaseHandler, IRequestHandler<CommitCustomerCommand, Unit>
    {
        private readonly ICustomerRepository _repository;
        public CommitCustomerHandler(
            ICustomerRepository repository,
            IMapper mapper)
            : base(mapper)
            => this._repository = repository;


        public async Task<Unit> Handle(CommitCustomerCommand request, CancellationToken cancellationToken)
        {
            var entity = this._mapper.Map<Customer>(request.Customer);

            this._repository.Add(entity);
            await _repository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}