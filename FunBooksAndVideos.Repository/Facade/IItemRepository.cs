using FunBooksAndVideos.DAL.Entities;

namespace FunBooksAndVideos.Repository.Facade
{
    public interface IItemRepository : IRepository
    {
        Task<Item> GetByIdAsync(Guid id);
        Task<IEnumerable<Item>> GetAllAsync();
        void Add(Item entity);
        void Update(Item entity);
        void Delete(Item entity);
    }
}
