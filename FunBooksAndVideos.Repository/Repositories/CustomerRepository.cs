using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.DAL.Entities;
using FunBooksAndVideos.Repository.Facade;
using Microsoft.EntityFrameworkCore;

namespace FunBooksAndVideos.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly FunDbContext _dbContext;

        public CustomerRepository(FunDbContext dbContext)
            => _dbContext = dbContext;

        public void Add(Customer entity) 
            => this._dbContext.Customers.Add(entity);

        public void Update(Customer entity)
            => this._dbContext.Customers.Update(entity);
        
        public void Delete(Customer entity)
            => this._dbContext.Customers.Remove(entity);

        public async Task<Customer> GetByIdAsync(Guid id)
            => await this._dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<Customer>> GetAllAsync()
            => await this._dbContext.Customers.ToListAsync();

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
            => await this._dbContext.SaveChangesAsync(cancellationToken);

    }
}
