using AutoMapper;
using FunBooksAndVideos.DAL.Entities;
using FunBooksAndVideos.Repository.Facade;
using FunBooksAndVideos.WebApi.Handlers.Base;
using FunBooksAndVideos.WebApi.Queries;
using MediatR;

namespace FunBooksAndVideos.WebApi.Handlers
{
    public class GetPurchaseOrderByIdHandler
        : BaseHandler, IRequestHandler<GetPurchaseOrderByIdQuery, DTOs.PurchaseOrder>
    {
        private readonly IPurchaseOrderRepository _repository;
        public GetPurchaseOrderByIdHandler(
            IPurchaseOrderRepository repository,
            IMapper mapper)
            : base(mapper)
            => this._repository = repository;

        public async Task<DTOs.PurchaseOrder> Handle(
            GetPurchaseOrderByIdQuery request,
            CancellationToken cancellationToken)
            => this._mapper.Map<DTOs.PurchaseOrder>(
                await this._repository.GetByIdAsync(request.Id)
               );
    }
}
