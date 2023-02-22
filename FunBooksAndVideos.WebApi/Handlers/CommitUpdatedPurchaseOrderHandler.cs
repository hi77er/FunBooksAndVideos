using AutoMapper;
using FunBooksAndVideos.DAL.Entities;
using FunBooksAndVideos.Repository.Facade;
using FunBooksAndVideos.WebApi.Commands;
using FunBooksAndVideos.WebApi.Handlers.Base;
using MediatR;

namespace FunBooksAndVideos.WebApi.Handlers
{
    public class CommitUpdatedPurchaseOrderHandler
        : BaseHandler, IRequestHandler<CommitUpdatedPurchaseOrderCommand, Unit>
    {
        private readonly IPurchaseOrderRepository _repository;
        public CommitUpdatedPurchaseOrderHandler(
            IPurchaseOrderRepository repository,
            IMapper mapper)
            : base(mapper)
            => this._repository = repository;


        public async Task<Unit> Handle(CommitUpdatedPurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            var entity = this._mapper.Map<PurchaseOrder>(request.PurchaseOrder);

            this._repository.Update(entity);
            await this._repository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
