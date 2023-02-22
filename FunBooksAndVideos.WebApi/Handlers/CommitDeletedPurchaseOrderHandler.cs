using AutoMapper;
using FunBooksAndVideos.Repository.Facade;
using FunBooksAndVideos.WebApi.Commands;
using FunBooksAndVideos.WebApi.Handlers.Base;
using MediatR;

namespace FunBooksAndVideos.WebApi.Handlers
{
    public class CommitDeletedPurchaseOrderHandler
        : BaseHandler, IRequestHandler<CommitDeletedPurchaseOrderCommand, Unit>
    {
        private readonly IPurchaseOrderRepository _repository;
        public CommitDeletedPurchaseOrderHandler(
            IPurchaseOrderRepository repository,
            IMapper mapper)
            : base(mapper)
            => this._repository = repository;


        public async Task<Unit> Handle(CommitDeletedPurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            var purchaseOrder = await this._repository
                .GetByIdAsync(request.Id);

            this._repository.Delete(purchaseOrder);
            await this._repository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
