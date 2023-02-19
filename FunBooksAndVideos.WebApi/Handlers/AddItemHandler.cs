
using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.WebApi.Commands;
using MediatR;

namespace FunBooksAndVideos.WebApi.Handlers
{
    public class AddItemHandler : IRequestHandler<AddItemCommand, Unit>
    {
        private readonly FunDbContext _dbContext;
        public AddItemHandler(FunDbContext dbContext) => _dbContext = dbContext;
        
        public async Task<Unit> Handle(AddItemCommand request, CancellationToken cancellationToken)
        {
            _dbContext.Items.Add(request.Item);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
