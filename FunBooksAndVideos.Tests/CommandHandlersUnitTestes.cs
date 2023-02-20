using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.DAL.Entities;
using FunBooksAndVideos.DAL.Entities.Base;
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
            var item1 = new Item() { Id = Guid.NewGuid(), ItemType = DAL.Entities.Enums.ItemType.Physical, Name = "1984, Orwel G.", Price = 2.4m };
            var item2 = new Item() { Id = Guid.NewGuid(), ItemType = DAL.Entities.Enums.ItemType.NonPhysical, Name = "The Matrix", Price = 3.6m };
            var item3 = new Item() { Id = Guid.NewGuid(), ItemType = DAL.Entities.Enums.ItemType.NonPhysical, Name = "Book Club", Price = 6.3m, SubscriptionType = DAL.Entities.Enums.SubscriptionType.Monthly };

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
        public async Task AddOrderCommand_OrderIsInContext()
        {
            // Arrange
            var items = new List<Item>(
                new Item[]
                {
                    new Item() { Id = Guid.NewGuid(), ItemType = DAL.Entities.Enums.ItemType.Physical, Name = "1984, Orwel G.", Price = 2.4m },
                    new Item() { Id = Guid.NewGuid(), ItemType = DAL.Entities.Enums.ItemType.NonPhysical, Name = "The Matrix", Price = 3.6m },
                    new Item() { Id = Guid.NewGuid(), ItemType = DAL.Entities.Enums.ItemType.NonPhysical, Name = "Book Club", Price = 6.3m, SubscriptionType = DAL.Entities.Enums.SubscriptionType.Monthly },
                }
            );
            var order = new PurchaseOrder() { Id = Guid.NewGuid(), CustomerId = Guid.NewGuid(), OrderNumber = "12345" };
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

            var orders = new List<PurchaseOrder>(new PurchaseOrder[] { order })
                .AsQueryable();

            this.ConfigureMockDbSet(_mockOrdersSet, orders);
            _mockDbContext.Setup(m => m.PurchaseOrders).Returns(_mockOrdersSet.Object);

            var command = new CommitPurchaseOrderCommand(order);
            var handler = new CommitPurchaseOrderHandler(_mockDbContext.Object);

            // Act
            await handler.Handle(command, It.IsAny<CancellationToken>());

            _mockOrdersSet.Verify(m => m.Add(It.IsAny<PurchaseOrder>()), Times.Once());
            _mockDbContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
            _mockDbContext.Verify(m => m.SaveChanges(), Times.Never());

            Xunit.Assert.Contains(order, _mockDbContext.Object.PurchaseOrders);
        }

        [Fact]
        public async Task CommitCustomerCommand_CustomerIsInDb()
        {
            // Arrange
            var customer = new Customer()
            {
                Id = Guid.NewGuid(),
                Email = "me.test@live.com",
                FirstName = "Test",
                LastName = "Test",
                Address = "Test",
                Phone = "Test"
            };

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
