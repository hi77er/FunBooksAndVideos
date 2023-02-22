using FunBooksAndVideos.DAL.Entities;

namespace FunBooksAndVideos.Repository.Facade
{
    public interface IPurchaseOrderRepository : IRepository
    {
        Task<PurchaseOrder> GetByIdAsync(Guid id);
        Task<IEnumerable<PurchaseOrder>> GetAllAsync();
        void Add(PurchaseOrder entity);
        void Update(PurchaseOrder entity);
        void Delete(PurchaseOrder entity);
    }
}
