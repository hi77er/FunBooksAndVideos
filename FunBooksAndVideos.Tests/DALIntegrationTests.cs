using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.DAL.Entities;
using FunBooksAndVideos.DAL.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace FunBooksAndVideos.Tests
{
    [TestClass]
    public class DALIntegrationTests
    {
        const string connStr = "Server=tcp:funbooksandvideos-sql-server.database.windows.net,1433;Initial Catalog=funbooksandvideos-db;Persist Security Info=False;User ID=kkrastev;Password=!234Qwer;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        private FunDbContext _dbContext;

        [TestInitialize]
        public void TestInitialize()
        {
            DbContextOptionsBuilder<FunDbContext> optionsBuilder =
                    new DbContextOptionsBuilder<FunDbContext>()
                        .UseSqlServer(connStr);

            _dbContext = new FunDbContext(optionsBuilder.Options);
        }

        [TestMethod]
        public void TestMethod1()
        {
            var customer = new Customer()
            {
                Email = "sample.me@hotmail.com",
                FirstName = "George",
                LastName = "Clooney",
                Phone = "+3597263582",
                Address = "72, Parker str., London"
            };
            _dbContext.Customers.Add(customer);

            var item1 = new Item() { Id = Guid.NewGuid(), Name = "1984 by George Orwel", Price = 2.5m, ItemType = ItemType.Physical };
            item1.Attributes = new List<ItemAttribute>(
                new ItemAttribute[]
                {
                    new ItemAttribute() { ItemId = item1.Id, Item = item1, AttributeType = AttributeType.Text, Name = "Name", Value = "1984", },
                    new ItemAttribute() { ItemId = item1.Id, Item = item1, AttributeType = AttributeType.Text, Name = "Author", Value = "George Orwel", },
                    new ItemAttribute() { ItemId = item1.Id, Item = item1, AttributeType = AttributeType.Text, Name = "Publisher", Value = "Penguin", },
                    new ItemAttribute() { ItemId = item1.Id, Item = item1, AttributeType = AttributeType.Date, Name = "Published", Value = "1949-06-01T00:00:00+00:00", },
                }
            );

            var item2 = new Item() { Id = Guid.NewGuid(), Name = "Die Hard", Price = 2.5m, ItemType = ItemType.NonPhysical };
            item2.Attributes = new List<ItemAttribute>(
                new ItemAttribute[]
                {
                    new ItemAttribute() { ItemId = item2.Id, Item = item2, AttributeType = AttributeType.Text, Name = "Name", Value = "Die hard", },
                    new ItemAttribute() { ItemId = item2.Id, Item = item2, AttributeType = AttributeType.Text, Name = "Director", Value = "John McTiernan", },
                    new ItemAttribute() { ItemId = item2.Id, Item = item2, AttributeType = AttributeType.Text, Name = "Stars", Value = "Bruce Willis, Alan Rickman, Bonnie Bedelia", },
                    new ItemAttribute() { ItemId = item2.Id, Item = item2, AttributeType = AttributeType.Decimal, Name = "RatingInIMDB", Value = "8,2", },
                    new ItemAttribute() { ItemId = item2.Id, Item = item2, AttributeType = AttributeType.Integer, Name = "DurationMinutes", Value = "132", },
                }
            );

            var item3 = new Item() { Id = Guid.NewGuid(), Name = "Book Club Monthly Membership", Price = 2.5m, ItemType = ItemType.NonPhysical, SubscriptionType = SubscriptionType.Monthly };
            item3.Attributes = new List<ItemAttribute>(
                new ItemAttribute[]
                {
                    new ItemAttribute() { ItemId = item3.Id, Item = item3, AttributeType = AttributeType.Text, Name = "Name", Value = "Book Club Membership", },
                    new ItemAttribute() { ItemId = item3.Id, Item = item3, AttributeType = AttributeType.Text, Name = "Type", Value = "Monthly", }
                }
            );
            _dbContext.Items.AddRange(item1, item2, item3);
            _dbContext.SaveChanges();

            var order1 = new PurchaseOrder()
            {
                Id = Guid.NewGuid(),
                CustomerId = customer.Id,
                Customer = customer,
                OrderNumber = "743254"
            };
            order1.PurchaseOrderItems = new List<PurchaseOrderItem>(
                new PurchaseOrderItem[] {
                    new PurchaseOrderItem() { ItemId = item1.Id, Item = item1, OrderId = order1.Id, Amount = 2 },
                    new PurchaseOrderItem() { ItemId = item2.Id, Item = item2, OrderId = order1.Id  },
                    new PurchaseOrderItem() { ItemId = item3.Id, Item = item3, OrderId = order1.Id },
                }
            );
            var physicalItems = order1.PurchaseOrderItems
                .Where(x => x.Item.ItemType == ItemType.Physical);

            if (physicalItems.Any())
                order1.ShippingSlip = new ShippingSlip() { OrderId = order1.Id, Order = order1, ShippingCarrier = ShippingCarrier.UPS };

            var subscriptions = order1.PurchaseOrderItems
                .Where(x => x.Item.SubscriptionType != null);

            if (physicalItems.Any())
                customer.Memberships = order1.PurchaseOrderItems
                .Where(x => x.Item.SubscriptionType != null)
                .Select(x => new Membership()
                {
                    CustomerId = customer.Id,
                    ItemId = x.Item.Id,
                    EndDate =
                        x.Item.SubscriptionType == SubscriptionType.Monthly
                            ? DateTime.UtcNow.Date.AddMonths(1)
                            : (
                                x.Item.SubscriptionType == SubscriptionType.Annual
                                    ? DateTime.UtcNow.Date.AddYears(1)
                                    : DateTime.UtcNow.Date.AddYears(100)
                            )
                })
                .ToList();
            
            var countBefore = _dbContext.PurchaseOrders.Count();

            _dbContext.PurchaseOrders.Add(order1);
            _dbContext.Customers.Update(customer);
            _dbContext.SaveChanges();

            var order2 = new PurchaseOrder()
            {
                Id = Guid.NewGuid(),
                CustomerId = customer.Id,
                Customer = customer,
                OrderNumber = "982125"
            };
            order2.PurchaseOrderItems = new List<PurchaseOrderItem>(
                new PurchaseOrderItem[] {
                    new PurchaseOrderItem() { ItemId = item2.Id, Item = item2, OrderId = order2.Id },
                }
            );
            var physicalItems2 = order2
                .PurchaseOrderItems
                .Select(x => x.Item.ItemType)
                .Where(x => x == ItemType.Physical);

            if (physicalItems2.Any())
                order2.ShippingSlip = new ShippingSlip() { OrderId = order2.Id, Order = order2, ShippingCarrier = ShippingCarrier.UPS };

            _dbContext.PurchaseOrders.Add(order2);
            _dbContext.SaveChanges();

            var orders = _dbContext.PurchaseOrders.ToList();

            Assert.AreEqual(countBefore + 2, orders.Count);
        }

    }
}