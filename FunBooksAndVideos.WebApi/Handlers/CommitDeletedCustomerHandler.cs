using AutoMapper;
using FunBooksAndVideos.Repository.Facade;
using FunBooksAndVideos.WebApi.Commands;
using FunBooksAndVideos.WebApi.Handlers.Base;
using MediatR;

namespace FunBooksAndVideos.WebApi.Handlers
{
    public class CommitDeletedCustomerHandler
        : BaseHandler, IRequestHandler<CommitDeletedCustomerCommand, Unit>
    {
        private readonly ICustomerRepository _repository;
        public CommitDeletedCustomerHandler(
            ICustomerRepository repository,
            IMapper mapper)
            : base(mapper)
            => this._repository = repository;


        public async Task<Unit> Handle(CommitDeletedCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await this._repository.GetByIdAsync(request.Id);

            this._repository.Delete(customer);
            await this._repository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
