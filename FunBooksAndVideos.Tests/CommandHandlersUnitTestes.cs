using FunBooksAndVideos.DAL.Context;
using Entity = FunBooksAndVideos.DAL.Entities;
using FunBooksAndVideos.DAL.Entities.Base;
using FunBooksAndVideos.DAL.Entities.Enums;
using FunBooksAndVideos.DTOs;
using FunBooksAndVideos.WebApi.Commands;
using FunBooksAndVideos.WebApi.Handlers;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using AutoMapper;
using FunBooksAndVideos.Repository.Facade;
using FunBooksAndVideos.Repository;
using FunBooksAndVideos.WebApi.Mapper;

namespace FunBooksAndVideos.Tests
{
    public class CommandHandlersUnitTestes
    {
        private readonly IMapper _mapper;
        private readonly IItemRepository _itemRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly Mock<FunDbContext> _mockDbContext;
        private readonly Mock<DbSet<Entity.Customer>> _mockCustomersSet;
        private readonly Mock<DbSet<Entity.Item>> _mockItemsSet;
        private readonly Mock<DbSet<Entity.PurchaseOrder>> _mockOrdersSet;

        private readonly Entity.Customer customer = new Entity.Customer()
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
            var config = new MapperConfiguration(opt =>
            {
                opt.AddProfile(new EntityDTOProfile());
            });
            _mapper = config.CreateMapper();

            var options = new DbContextOptionsBuilder<FunDbContext>()
                .UseInMemoryDatabase(databaseName: "MovieListDatabase")
                .Options;

            _mockDbContext = new Mock<FunDbContext>(options);
            _mockCustomersSet = new Mock<DbSet<Entity.Customer>>();
            _mockOrdersSet = new Mock<DbSet<Entity.PurchaseOrder>>();
            _mockItemsSet = new Mock<DbSet<Entity.Item>>();

            _customerRepository = new CustomerRepository(_mockDbContext.Object);
            _itemRepository = new ItemRepository(_mockDbContext.Object);
            _purchaseOrderRepository = new PurchaseOrderRepository(_mockDbContext.Object);
        }

        [Fact]
        public async Task CommitItemCommand_ItemIsInDb()
        {
            // Arrange
            var item1 = new Entity.Item() { Id = Guid.NewGuid(), ItemType = ItemType.Physical, Name = "1984, Orwel G.", Price = 2.4m };
            var item2 = new Entity.Item() { Id = Guid.NewGuid(), ItemType = ItemType.NonPhysical, Name = "The Matrix", Price = 3.6m };
            var item3Id = Guid.NewGuid();
            var item3 = new Entity.Item()
            {
                Id = item3Id,
                ItemType = ItemType.NonPhysical,
                Name = "Book Club",
                Price = 6.3m,
                SubscriptionType = SubscriptionType.Monthly,
                Attributes = new List<Entity.ItemAttribute>(
                    new Entity.ItemAttribute[]
                    {
                        new Entity.ItemAttribute()
                        {
                            Id = Guid.NewGuid(),
                            ItemId = item3Id,
                            Name = "Pages",
                            AttributeType = AttributeType.Integer,
                            Value = "540"
                        }
                    }
                )
            };

            var items = new List<Entity.Item>(new Entity.Item[] { item1, item2, item3 })
                .AsQueryable();

            this.ConfigureMockDbSet(_mockItemsSet, items);
            _mockDbContext.Setup(m => m.Items).Returns(_mockItemsSet.Object);

            var dtoItem3 = this._mapper.Map<Item>(item3);
            var command = new CommitItemCommand(dtoItem3);
            var handler = new CommitItemHandler(_itemRepository, this._mapper);

            // Act
            await handler.Handle(command, It.IsAny<CancellationToken>());

            _mockItemsSet.Verify(m => m.Add(It.IsAny<Entity.Item>()), Times.Once());
            _mockDbContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
            _mockDbContext.Verify(m => m.SaveChanges(), Times.Never());

            Xunit.Assert.Contains(item3, _mockDbContext.Object.Items);
        }

        [Fact]
        public async Task CommitCustomerCommand_CustomerIsInDb()
        {
            // Arrange
            var customers = new List<Entity.Customer>(new Entity.Customer[] { customer })
                .AsQueryable();

            this.ConfigureMockDbSet(_mockCustomersSet, customers);
            _mockDbContext.Setup(m => m.Customers).Returns(_mockCustomersSet.Object);

            var dtoCustomer = this._mapper.Map<Customer>(customer);
            var command = new CommitCustomerCommand(dtoCustomer);
            var handler = new CommitCustomerHandler(_customerRepository, _mapper);

            // Act
            await handler.Handle(command, It.IsAny<CancellationToken>());

            _mockCustomersSet.Verify(m => m.Add(It.IsAny<Entity.Customer>()), Times.Once());
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
            var items = new List<Entity.Item>(
                new Entity.Item[]
                {
                    new Entity.Item() { Id = Guid.NewGuid(), ItemType = itemType, Name = name, Price = price, SubscriptionType = subscriptionType },
                }
            );
            var order = new Entity.PurchaseOrder() { Id = Guid.NewGuid(), CustomerId = customer.Id, OrderNumber = "12345" };
            var purchaseItems = items
                .Select(x => new Entity.PurchaseOrderItem()
                {
                    OrderId = order.Id,
                    Order = order,
                    ItemId = x.Id,
                    Item = x,
                    Amount = 1
                })
                .ToArray();
            order.PurchaseOrderItems = purchaseItems;

            var customers = new List<Entity.Customer>(new Entity.Customer[] { customer }).AsQueryable();
            var orders = new List<Entity.PurchaseOrder>(new Entity.PurchaseOrder[] { order }).AsQueryable();

            this.ConfigureMockDbSet(_mockCustomersSet, customers);
            this.ConfigureMockDbSet(_mockOrdersSet, orders);
            _mockDbContext.Setup(m => m.Customers).Returns(_mockCustomersSet.Object);
            _mockDbContext.Setup(m => m.PurchaseOrders).Returns(_mockOrdersSet.Object);

            var dtoOrder = this._mapper.Map<PurchaseOrder>(order);
            var command = new CommitPurchaseOrderCommand(dtoOrder);
            var handler = new CommitPurchaseOrderHandler(_customerRepository, _purchaseOrderRepository, _mapper);

            // Act
            await handler.Handle(command, It.IsAny<CancellationToken>());

            _mockOrdersSet.Verify(m => m.Add(It.IsAny<Entity.PurchaseOrder>()), Times.Once());
            _mockDbContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
            _mockDbContext.Verify(m => m.SaveChanges(), Times.Never());

            Xunit.Assert.Contains(order, _mockDbContext.Object.PurchaseOrders);
        }

        [Theory]
        [InlineData("Books Club, Monthly", ItemType.NonPhysical, SubscriptionType.Monthly, 14.8)]
        [InlineData("Video Club, Monthly", ItemType.NonPhysical, SubscriptionType.Monthly, 63.3)]
        [InlineData("Video Club, Annual", ItemType.NonPhysical, SubscriptionType.Annual, 16.3)]
        public async Task AddOrderCommand_NonPhysicalItem_MembershipActivated(string name, ItemType itemType, SubscriptionType? subscriptionType, decimal price)
        {
            // Arrange
            var items = new List<Entity.Item>(
                new Entity.Item[]
                {
                    new Entity.Item() { Id = Guid.NewGuid(), ItemType = itemType, Name = name, Price = price, SubscriptionType = subscriptionType },
                }
            );
            var order = new Entity.PurchaseOrder() { Id = Guid.NewGuid(), CustomerId = customer.Id, OrderNumber = "12345" };
            var purchaseItems = items
                .Select(x => new Entity.PurchaseOrderItem()
                {
                    OrderId = order.Id,
                    Order = order,
                    ItemId = x.Id,
                    Item = x,
                    Amount = 1
                })
                .ToArray();
            order.PurchaseOrderItems = purchaseItems;

            var customers = new List<Entity.Customer>(new Entity.Customer[] { customer }).AsQueryable();
            var orders = new List<Entity.PurchaseOrder>(new Entity.PurchaseOrder[] { order }).AsQueryable();

            this.ConfigureMockDbSet(_mockCustomersSet, customers);
            this.ConfigureMockDbSet(_mockOrdersSet, orders);
            _mockDbContext.Setup(m => m.Customers).Returns(_mockCustomersSet.Object);
            _mockDbContext.Setup(m => m.PurchaseOrders).Returns(_mockOrdersSet.Object);

            var dtoOrder = this._mapper.Map<PurchaseOrder?>(order);
            var command = new CommitPurchaseOrderCommand(dtoOrder);
            var handler = new CommitPurchaseOrderHandler(_customerRepository, _purchaseOrderRepository, _mapper);

            // Act
            await handler.Handle(command, It.IsAny<CancellationToken>());

            _mockOrdersSet.Verify(m => m.Add(It.IsAny<Entity.PurchaseOrder>()), Times.Once());
            _mockDbContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
            _mockDbContext.Verify(m => m.SaveChanges(), Times.Never());

            Xunit.Assert.Contains(order, _mockDbContext.Object.PurchaseOrders);
        }

        [Theory]
        [InlineData("Harry Potter 2", ItemType.NonPhysical, null, 7.8)]
        [InlineData("Ice Age", ItemType.NonPhysical, null, 9.3)]
        public async Task AddOrderCommand_NonPhysicalNonSubscriptionItem(string name, ItemType itemType, SubscriptionType? subscriptionType, decimal price)
        {
            // Arrange
            var items = new List<Entity.Item>(
                new Entity.Item[]
                {
                    new Entity.Item() { Id = Guid.NewGuid(), ItemType = itemType, Name = name, Price = price, SubscriptionType = subscriptionType },
                }
            );
            var order = new Entity.PurchaseOrder() { Id = Guid.NewGuid(), CustomerId = customer.Id, OrderNumber = "12345" };
            var purchaseItems = items
                .Select(x => new Entity.PurchaseOrderItem()
                {
                    OrderId = order.Id,
                    Order = order,
                    ItemId = x.Id,
                    Item = x,
                    Amount = 1
                })
                .ToArray();
            order.PurchaseOrderItems = purchaseItems;

            var customers = new List<Entity.Customer>(new Entity.Customer[] { customer }).AsQueryable();
            var orders = new List<Entity.PurchaseOrder>(new Entity.PurchaseOrder[] { order }).AsQueryable();

            this.ConfigureMockDbSet(_mockCustomersSet, customers);
            this.ConfigureMockDbSet(_mockOrdersSet, orders);
            _mockDbContext.Setup(m => m.Customers).Returns(_mockCustomersSet.Object);
            _mockDbContext.Setup(m => m.PurchaseOrders).Returns(_mockOrdersSet.Object);

            var dtoOrder = this._mapper.Map<PurchaseOrder>(order);
            var command = new CommitPurchaseOrderCommand(dtoOrder);
            var handler = new CommitPurchaseOrderHandler(_customerRepository, _purchaseOrderRepository, _mapper);

            // Act
            await handler.Handle(command, It.IsAny<CancellationToken>());

            _mockOrdersSet.Verify(m => m.Add(It.IsAny<Entity.PurchaseOrder>()), Times.Once());
            _mockDbContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
            _mockDbContext.Verify(m => m.SaveChanges(), Times.Never());

            Xunit.Assert.Contains(order, _mockDbContext.Object.PurchaseOrders);
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
