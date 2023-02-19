
using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.WebApi.Commands;
using MediatR;

namespace FunBooksAndVideos.WebApi.Handlers
{
    public class UpdateItemHandler : IRequestHandler<UpdateItemCommand, Unit>
    {
        private readonly FunDbContext _dbContext;
        public UpdateItemHandler(FunDbContext dbContext) => _dbContext = dbContext;
        
        public async Task<Unit> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            _dbContext.Items.Update(request.Item);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
