using FunBooksAndVideos.DAL.Entities;
using FunBooksAndVideos.DAL.Entities.Base;
using FunBooksAndVideos.DAL.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace FunBooksAndVideos.DAL.Context
{
    public class FunDbContext : DbContext
    {
        public FunDbContext(DbContextOptions<FunDbContext> options)
            : base(options)
        { }

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Item> Items => Set<Item>();
        public DbSet<ItemAttribute> ItemAttributes => base.Set<ItemAttribute>();
        public DbSet<PurchaseOrder> PurchaseOrders => Set<PurchaseOrder>();
        public DbSet<ShippingSlip> ShippingSlips => Set<ShippingSlip>();
        public DbSet<Membership> Memberships => Set<Membership>();

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
