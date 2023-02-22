using AutoMapper;
using FunBooksAndVideos.DAL.Entities;
using FunBooksAndVideos.Repository.Facade;
using FunBooksAndVideos.WebApi.Commands;
using FunBooksAndVideos.WebApi.Handlers.Base;
using MediatR;

namespace FunBooksAndVideos.WebApi.Handlers
{
    public class CommitItemHandler
        : BaseHandler, IRequestHandler<CommitItemCommand, Unit>
    {
        private readonly IItemRepository _repository;
        public CommitItemHandler(
            IItemRepository repository,
            IMapper mapper)
            : base(mapper)
            => this._repository = repository;


        public async Task<Unit> Handle(CommitItemCommand request, CancellationToken cancellationToken)
        {
            var entity = this._mapper.Map<Item>(request.Item);

            this._repository.Add(entity);
            await this._repository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
