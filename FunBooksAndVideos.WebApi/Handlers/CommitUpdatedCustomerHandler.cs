using AutoMapper;
using FunBooksAndVideos.DAL.Entities;
using FunBooksAndVideos.Repository.Facade;
using FunBooksAndVideos.WebApi.Commands;
using FunBooksAndVideos.WebApi.Handlers.Base;
using MediatR;

namespace FunBooksAndVideos.WebApi.Handlers
{
    public class CommitUpdatedCustomerHandler
        : BaseHandler, IRequestHandler<CommitUpdatedCustomerCommand, Unit>
    {
        private readonly ICustomerRepository _repository;
        public CommitUpdatedCustomerHandler(
            ICustomerRepository repository,
            IMapper mapper)
            : base(mapper)
            => this._repository = repository;

        public async Task<Unit> Handle(CommitUpdatedCustomerCommand request, CancellationToken cancellationToken)
        {
            var entity = this._mapper.Map<Customer>(request.Customer);

            this._repository.Update(entity);
            await this._repository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
