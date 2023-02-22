using FunBooksAndVideos.DAL.Entities;

namespace FunBooksAndVideos.Repository.Facade
{
    public interface ICustomerRepository : IRepository
    {
        Task<Customer> GetByIdAsync(Guid id);
        Task<IEnumerable<Customer>> GetAllAsync();
        void Add(Customer entity);
        void Update(Customer entity);
        void Delete(Customer entity);
    }
}
