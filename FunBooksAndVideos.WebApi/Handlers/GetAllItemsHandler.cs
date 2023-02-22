using AutoMapper;
using FunBooksAndVideos.DAL.Entities;
using FunBooksAndVideos.Repository.Facade;
using FunBooksAndVideos.WebApi.Handlers.Base;
using FunBooksAndVideos.WebApi.Queries;
using MediatR;

namespace FunBooksAndVideos.WebApi.Handlers
{
    public class GetAllItemsHandler
        : BaseHandler, IRequestHandler<GetAllItemsQuery, IEnumerable<DTOs.Item>>
    {
        private readonly IItemRepository _repository;
        public GetAllItemsHandler(
            IItemRepository repository,
            IMapper mapper)
            : base(mapper)
            => this._repository = repository;


        public async Task<IEnumerable<DTOs.Item>> Handle(
            GetAllItemsQuery request,
            CancellationToken cancellationToken)
            => this._mapper.Map<IEnumerable<DTOs.Item>>(
                await this._repository.GetAllAsync()
               );

    }
}
