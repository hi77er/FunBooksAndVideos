using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.DAL.Entities;
using FunBooksAndVideos.Repository.Facade;
using Microsoft.EntityFrameworkCore;

namespace FunBooksAndVideos.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly FunDbContext _dbContext;

        public ItemRepository(FunDbContext dbContext) => _dbContext = dbContext;

        public void Add(Item entity)
            => this._dbContext.Items.Add(entity);

        public void Update(Item entity)
            => this._dbContext.Items.Update(entity);

        public void Delete(Item entity)
            => this._dbContext.Items.Remove(entity);

        public async Task<Item> GetByIdAsync(Guid id)
            => await this._dbContext.Items.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<Item>> GetAllAsync()
            => await this._dbContext.Items.ToListAsync();

        public async Task SaveChangesAsync(CancellationToken cancellationToken) 
            => await this._dbContext.SaveChangesAsync(cancellationToken);
    }
}
