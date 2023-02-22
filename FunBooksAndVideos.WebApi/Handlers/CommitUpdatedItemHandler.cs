using AutoMapper;
using FunBooksAndVideos.DAL.Entities;
using FunBooksAndVideos.Repository.Facade;
using FunBooksAndVideos.WebApi.Commands;
using FunBooksAndVideos.WebApi.Handlers.Base;
using MediatR;

namespace FunBooksAndVideos.WebApi.Handlers
{
    public class CommitUpdatedItemHandler
        : BaseHandler, IRequestHandler<CommitUpdatedItemCommand, Unit>
    {
        private readonly IItemRepository _repository;
        public CommitUpdatedItemHandler(
            IItemRepository repository,
            IMapper mapper)
            : base(mapper)
            => this._repository = repository;


        public async Task<Unit> Handle(CommitUpdatedItemCommand request, CancellationToken cancellationToken)
        {
            var entity = this._mapper.Map<Item>(request.Item);

            this._repository.Update(entity);
            await this._repository.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
