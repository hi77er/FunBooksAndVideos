using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.DAL.Entities;
using FunBooksAndVideos.Repository.Facade;
using Microsoft.EntityFrameworkCore;

namespace FunBooksAndVideos.Repository
{
    public class PurchaseOrderRepository : IPurchaseOrderRepository
    {
        private readonly FunDbContext _dbContext;

        public PurchaseOrderRepository(FunDbContext dbContext)
            => _dbContext = dbContext;

        public void Add(PurchaseOrder entity)
            => this._dbContext.PurchaseOrders.Add(entity);

        public void Update(PurchaseOrder entity)
            => this._dbContext.PurchaseOrders.Update(entity);

        public void Delete(PurchaseOrder entity)
            => this._dbContext.PurchaseOrders.Remove(entity);

        public async Task<PurchaseOrder> GetByIdAsync(Guid id)
            => await this._dbContext
            .PurchaseOrders
            .Include(x => x.PurchaseOrderItems)
            .Include(x => x.ShippingSlip).FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<PurchaseOrder>> GetAllAsync()
            => await this._dbContext
            .PurchaseOrders
            .Include(x => x.PurchaseOrderItems)
            .Include(x => x.ShippingSlip)
            .ToListAsync();



        public async Task SaveChangesAsync(CancellationToken cancellationToken)
            => await this._dbContext.SaveChangesAsync(cancellationToken);
    }
}
