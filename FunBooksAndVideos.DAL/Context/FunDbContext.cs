using FunBooksAndVideos.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FunBooksAndVideos.DAL.Context
{
    public class FunDbContext : DbContext
    {
        public FunDbContext(DbContextOptions<FunDbContext> options)
            : base(options)
        { }

        public virtual DbSet<Customer> Customers => Set<Customer>();
        public virtual DbSet<Item> Items => Set<Item>();
        public virtual DbSet<ItemAttribute> ItemAttributes => base.Set<ItemAttribute>();
        public virtual DbSet<PurchaseOrder> PurchaseOrders => Set<PurchaseOrder>();
        public virtual DbSet<ShippingSlip> ShippingSlips => Set<ShippingSlip>();
        public virtual DbSet<Membership> Memberships => Set<Membership>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PurchaseOrderItem>()
                .HasKey(x => new { x.OrderId, x.ItemId });

            modelBuilder.Entity<PurchaseOrder>()
                .HasOne(a => a.ShippingSlip)
                .WithOne(a => a.Order)
                .HasForeignKey<ShippingSlip>(c => c.OrderId);
        }

    }
}
