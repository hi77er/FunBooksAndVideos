using AutoMapper;
using FunBooksAndVideos.Repository.Facade;
using FunBooksAndVideos.WebApi.Commands;
using FunBooksAndVideos.WebApi.Handlers.Base;
using MediatR;

namespace FunBooksAndVideos.WebApi.Handlers
{
    public class CommitDeletedItemHandler
        : BaseHandler, IRequestHandler<CommitDeletedItemCommand, Unit>
    {
        private readonly IItemRepository _repository;
        public CommitDeletedItemHandler(
            IItemRepository repository,
            IMapper mapper)
            : base(mapper)
            => this._repository = repository;

        public async Task<Unit> Handle(CommitDeletedItemCommand request, CancellationToken cancellationToken)
        {
            var item = await this._repository.GetByIdAsync(request.Id);

            this._repository.Delete(item);
            await this._repository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
