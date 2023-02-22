using AutoMapper;
using FunBooksAndVideos.DAL.Entities;
using FunBooksAndVideos.Repository.Facade;
using FunBooksAndVideos.WebApi.Handlers.Base;
using FunBooksAndVideos.WebApi.Queries;
using MediatR;

namespace FunBooksAndVideos.WebApi.Handlers
{
    public class GetItemByIdHandler
        : BaseHandler, IRequestHandler<GetItemByIdQuery, DTOs.Item>
    {
        private readonly IItemRepository _repository;
        public GetItemByIdHandler(
            IItemRepository repository,
            IMapper mapper)
            : base(mapper)
            => this._repository = repository;


        public async Task<DTOs.Item> Handle(
            GetItemByIdQuery request,
            CancellationToken cancellationToken)
            => this._mapper.Map<DTOs.Item>(
                await this._repository.GetByIdAsync(request.Id)
               );
    }
}
