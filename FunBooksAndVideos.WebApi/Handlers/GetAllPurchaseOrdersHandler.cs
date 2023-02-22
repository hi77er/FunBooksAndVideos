using AutoMapper;
using FunBooksAndVideos.DAL.Entities;
using FunBooksAndVideos.Repository.Facade;
using FunBooksAndVideos.WebApi.Handlers.Base;
using FunBooksAndVideos.WebApi.Queries;
using MediatR;

namespace FunBooksAndVideos.WebApi.Handlers
{
    public class GetAllPurchaseOrdersHandler
        : BaseHandler, IRequestHandler<GetAllPurchaseOrdersQuery, IEnumerable<DTOs.PurchaseOrder>>
    {
        private readonly IPurchaseOrderRepository _repository;
        public GetAllPurchaseOrdersHandler(
            IPurchaseOrderRepository repository,
            IMapper mapper)
            : base(mapper)
            => this._repository = repository;


        public async Task<IEnumerable<DTOs.PurchaseOrder>> Handle(
            GetAllPurchaseOrdersQuery request,
            CancellationToken cancellationToken)
            => this._mapper.Map<IEnumerable<DTOs.PurchaseOrder>>(
                await this._repository.GetAllAsync()
               );

    }
}
