using AutoMapper;
using FunBooksAndVideos.DAL.Entities;
using FunBooksAndVideos.Repository.Facade;
using FunBooksAndVideos.WebApi.Handlers.Base;
using FunBooksAndVideos.WebApi.Queries;
using MediatR;

namespace FunBooksAndVideos.WebApi.Handlers
{
    public class GetAllCustomersHandler
        : BaseHandler, IRequestHandler<GetAllCustomersQuery, IEnumerable<DTOs.Customer>>
    {
        private readonly ICustomerRepository _repository;
        public GetAllCustomersHandler(
            ICustomerRepository repository,
            IMapper mapper)
            : base(mapper)
            => this._repository = repository;


        public async Task<IEnumerable<DTOs.Customer>> Handle(
            GetAllCustomersQuery request,
            CancellationToken cancellationToken)
            => this._mapper.Map<IEnumerable<DTOs.Customer>>(
                await this._repository.GetAllAsync()
               );

    }
}
