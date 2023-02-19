
using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.WebApi.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FunBooksAndVideos.WebApi.Handlers
{
    public class DeleteItemHandler : IRequestHandler<DeleteItemCommand, Unit>
    {
        private readonly FunDbContext _dbContext;
        public DeleteItemHandler(FunDbContext dbContext) => _dbContext = dbContext;
        
        public async Task<Unit> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _dbContext
                .Items
                .FirstOrDefaultAsync(x => x.Id.Equals(request.Id));
            
            _dbContext.Items.Remove(item);
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}
