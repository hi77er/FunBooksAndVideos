using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.DAL.Entities;
using FunBooksAndVideos.DAL.Entities.Base;
using FunBooksAndVideos.DAL.Entities.Enums;
using FunBooksAndVideos.WebApi.Commands.DbCommands;
using FunBooksAndVideos.WebApi.Handlers.DbHandlers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Reflection.Metadata;
using Xunit;

namespace FunBooksAndVideos.Tests
{
    public class CommandHandlersUnitTestes
    {
        private readonly Mock<FunDbContext> _mockDbContext;
        private readonly Mock<DbSet<Customer>> _mockCustomersSet;
        private readonly Mock<DbSet<Item>> _mockItemsSet;
        private readonly Mock<DbSet<PurchaseOrder>> _mockOrdersSet;

        private readonly Customer customer = new Customer()
        {
            Id = Guid.NewGuid(),
            Email = It.IsAny<string>(),
            FirstName = It.IsAny<string>(),
            LastName = It.IsAny<string>(),
            Address = It.IsAny<string>(),
            Phone = It.IsAny<string>()
        };

        public CommandHandlersUnitTestes()
        {
            var options = new DbContextOptionsBuilder<FunDbContext>()
                .UseInMemoryDatabase(databaseName: "MovieListDatabase")
                .Options;

            _mockDbContext = new Mock<FunDbContext>(options);
            _mockCustomersSet = new Mock<DbSet<Customer>>();
            _mockOrdersSet = new Mock<DbSet<PurchaseOrder>>();
            _mockItemsSet = new Mock<DbSet<Item>>();
        }

        [Fact]
        public async Task CommitItemCommand_ItemIsInDb()
        {
            // Arrange
            var item1 = new Item() { Id = Guid.NewGuid(), ItemType = ItemType.Physical, Name = "1984, Orwel G.", Price = 2.4m };
            var item2 = new Item() { Id = Guid.NewGuid(), ItemType = ItemType.NonPhysical, Name = "The Matrix", Price = 3.6m };
            var item3 = new Item() { Id = Guid.NewGuid(), ItemType = ItemType.NonPhysical, Name = "Book Club", Price = 6.3m, SubscriptionType = SubscriptionType.Monthly };

            var items = new List<Item>(new Item[] { item1, item2, item3 })
                .AsQueryable();

            this.ConfigureMockDbSet(_mockItemsSet, items);
            _mockDbContext.Setup(m => m.Items).Returns(_mockItemsSet.Object);

            var command = new CommitItemCommand(item1);
            var handler = new CommitItemHandler(_mockDbContext.Object);

            // Act
            await handler.Handle(command, It.IsAny<CancellationToken>());

            _mockItemsSet.Verify(m => m.Add(It.IsAny<Item>()), Times.Once());
            _mockDbContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
            _mockDbContext.Verify(m => m.SaveChanges(), Times.Never());

            Xunit.Assert.Contains(item1, _mockDbContext.Object.Items);
        }

        [Fact]
        public async Task CommitCustomerCommand_CustomerIsInDb()
        {
            // Arrange
            var customers = new List<Customer>(new Customer[] { customer })
                .AsQueryable();

            this.ConfigureMockDbSet(_mockCustomersSet, customers);
            _mockDbContext.Setup(m => m.Customers).Returns(_mockCustomersSet.Object);

            var command = new CommitCustomerCommand(customer);
            var handler = new CommitCustomerHandler(_mockDbContext.Object);

            // Act
            await handler.Handle(command, It.IsAny<CancellationToken>());

            _mockCustomersSet.Verify(m => m.Add(It.IsAny<Customer>()), Times.Once());
            _mockDbContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
            _mockDbContext.Verify(m => m.SaveChanges(), Times.Never());

            Xunit.Assert.Contains(customer, _mockDbContext.Object.Customers);
        }

        [Theory]
        [InlineData("1984, Orwel G.", ItemType.Physical, null, 4.8)]
        [InlineData("Holy Bible", ItemType.Physical, null, 6.3)]
        public async Task AddOrderCommand_PhysicalItem_ContainsSlip(string name, ItemType itemType, SubscriptionType? subscriptionType, decimal price)
        {
            // Arrange
            var items = new List<Item>(
                new Item[]
                {
                    new Item() { Id = Guid.NewGuid(), ItemType = itemType, Name = name, Price = price, SubscriptionType = subscriptionType },
                }
            );
            var order = new PurchaseOrder() { Id = Guid.NewGuid(), CustomerId = customer.Id, OrderNumber = "12345" };
            var purchaseItems = items
                .Select(x => new PurchaseOrderItem()
                {
                    OrderId = order.Id,
                    Order = order,
                    ItemId = x.Id,
                    Item = x,
                    Amount = 1
                })
                .ToArray();
            order.Items = purchaseItems;

            var customers = new List<Customer>(new Customer[] { customer }).AsQueryable();
            var orders = new List<PurchaseOrder>(new PurchaseOrder[] { order }).AsQueryable();

            this.ConfigureMockDbSet(_mockCustomersSet, customers);
            this.ConfigureMockDbSet(_mockOrdersSet, orders);
            _mockDbContext.Setup(m => m.Customers).Returns(_mockCustomersSet.Object);
            _mockDbContext.Setup(m => m.PurchaseOrders).Returns(_mockOrdersSet.Object);

            var command = new CommitPurchaseOrderCommand(order);
            var handler = new CommitPurchaseOrderHandler(_mockDbContext.Object);

            // Act
            await handler.Handle(command, It.IsAny<CancellationToken>());

            _mockOrdersSet.Verify(m => m.Add(It.IsAny<PurchaseOrder>()), Times.Once());
            _mockDbContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
            _mockDbContext.Verify(m => m.SaveChanges(), Times.Never());

            Xunit.Assert.Contains(order, _mockDbContext.Object.PurchaseOrders);
            Xunit.Assert.True(_mockDbContext.Object.PurchaseOrders.All(x => x.ShippingSlip != null));
        }

        [Theory]
        [InlineData("Books Club, Monthly", ItemType.NonPhysical, SubscriptionType.Monthly, 14.8)]
        [InlineData("Video Club, Monthly", ItemType.NonPhysical, SubscriptionType.Monthly, 63.3)]
        [InlineData("Video Club, Annual", ItemType.NonPhysical, SubscriptionType.Annual, 16.3)]
        public async Task AddOrderCommand_NonPhysicalItem_MembershipActivated(string name, ItemType itemType, SubscriptionType? subscriptionType, decimal price)
        {
            // Arrange
            var items = new List<Item>(
                new Item[]
                {
                    new Item() { Id = Guid.NewGuid(), ItemType = itemType, Name = name, Price = price, SubscriptionType = subscriptionType },
                }
            );
            var order = new PurchaseOrder() { Id = Guid.NewGuid(), CustomerId = customer.Id, OrderNumber = "12345" };
            var purchaseItems = items
                .Select(x => new PurchaseOrderItem()
                {
                    OrderId = order.Id,
                    Order = order,
                    ItemId = x.Id,
                    Item = x,
                    Amount = 1
                })
                .ToArray();
            order.Items = purchaseItems;

            var customers = new List<Customer>(new Customer[] { customer }).AsQueryable();
            var orders = new List<PurchaseOrder>(new PurchaseOrder[] { order }).AsQueryable();

            this.ConfigureMockDbSet(_mockCustomersSet, customers);
            this.ConfigureMockDbSet(_mockOrdersSet, orders);
            _mockDbContext.Setup(m => m.Customers).Returns(_mockCustomersSet.Object);
            _mockDbContext.Setup(m => m.PurchaseOrders).Returns(_mockOrdersSet.Object);

            var command = new CommitPurchaseOrderCommand(order);
            var handler = new CommitPurchaseOrderHandler(_mockDbContext.Object);

            // Act
            await handler.Handle(command, It.IsAny<CancellationToken>());

            _mockOrdersSet.Verify(m => m.Add(It.IsAny<PurchaseOrder>()), Times.Once());
            _mockDbContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
            _mockDbContext.Verify(m => m.SaveChanges(), Times.Never());

            Xunit.Assert.Contains(order, _mockDbContext.Object.PurchaseOrders);
            Xunit.Assert.Contains(
                _mockDbContext.Object.Customers.First(x => x.Id.Equals(customer.Id)).Memberships,
                x => x.ItemId.Equals(items[0].Id)
            );
            Xunit.Assert.True(
                _mockDbContext.Object.PurchaseOrders
                .Where(x => x.Id.Equals(order.Id))
                .All(x => x.ShippingSlip == null));
        }

        [Theory]
        [InlineData("Harry Potter 2", ItemType.NonPhysical, null, 7.8)]
        [InlineData("Ice Age", ItemType.NonPhysical, null, 9.3)]
        public async Task AddOrderCommand_NonPhysicalNonSubscriptionItem(string name, ItemType itemType, SubscriptionType? subscriptionType, decimal price)
        {
            // Arrange
            var items = new List<Item>(
                new Item[]
                {
                    new Item() { Id = Guid.NewGuid(), ItemType = itemType, Name = name, Price = price, SubscriptionType = subscriptionType },
                }
            );
            var order = new PurchaseOrder() { Id = Guid.NewGuid(), CustomerId = customer.Id, OrderNumber = "12345" };
            var purchaseItems = items
                .Select(x => new PurchaseOrderItem()
                {
                    OrderId = order.Id,
                    Order = order,
                    ItemId = x.Id,
                    Item = x,
                    Amount = 1
                })
                .ToArray();
            order.Items = purchaseItems;

            var customers = new List<Customer>(new Customer[] { customer }).AsQueryable();
            var orders = new List<PurchaseOrder>(new PurchaseOrder[] { order }).AsQueryable();

            this.ConfigureMockDbSet(_mockCustomersSet, customers);
            this.ConfigureMockDbSet(_mockOrdersSet, orders);
            _mockDbContext.Setup(m => m.Customers).Returns(_mockCustomersSet.Object);
            _mockDbContext.Setup(m => m.PurchaseOrders).Returns(_mockOrdersSet.Object);

            var command = new CommitPurchaseOrderCommand(order);
            var handler = new CommitPurchaseOrderHandler(_mockDbContext.Object);

            // Act
            await handler.Handle(command, It.IsAny<CancellationToken>());

            _mockOrdersSet.Verify(m => m.Add(It.IsAny<PurchaseOrder>()), Times.Once());
            _mockDbContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
            _mockDbContext.Verify(m => m.SaveChanges(), Times.Never());

            var mems = _mockDbContext.Object.Customers
                    .FirstOrDefault(x => !x.Id.Equals(customer.Id))?
                        .Memberships;

            Xunit.Assert.Contains(order, _mockDbContext.Object.PurchaseOrders);
            Xunit.Assert.True(
                _mockDbContext.Object.Customers
                    .First(x => x.Id.Equals(customer.Id))
                        .Memberships.Count() == 0
                    );
            Xunit.Assert.True(
                _mockDbContext.Object.PurchaseOrders
                .Where(x => x.Id.Equals(order.Id))
                .All(x => x.ShippingSlip == null));
        }

        private void ConfigureMockDbSet<TEntity>(Mock<DbSet<TEntity>> mockDbSet, IQueryable<TEntity> queryable)
            where TEntity : EntityBase
        {
            mockDbSet.As<IQueryable<TEntity>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockDbSet.As<IQueryable<TEntity>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockDbSet.As<IQueryable<TEntity>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockDbSet.As<IQueryable<TEntity>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
        }
    }

}
