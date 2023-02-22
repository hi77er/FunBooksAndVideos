using AutoMapper;
using FunBooksAndVideos.DAL.Entities;
using FunBooksAndVideos.Repository.Facade;
using FunBooksAndVideos.WebApi.Handlers.Base;
using FunBooksAndVideos.WebApi.Queries;
using MediatR;

namespace FunBooksAndVideos.WebApi.Handlers
{
    public class GetCustomerByIdHandler
        : BaseHandler, IRequestHandler<GetCustomerByIdQuery, DTOs.Customer>
    {
        private readonly ICustomerRepository _repository;
        public GetCustomerByIdHandler(
            ICustomerRepository repository,
            IMapper mapper)
            : base(mapper)
            => this._repository = repository;


        public async Task<DTOs.Customer> Handle(
            GetCustomerByIdQuery request,
            CancellationToken cancellationToken)
            => this._mapper.Map<DTOs.Customer>(
                await this._repository.GetByIdAsync(request.Id)
               );
    }
}
