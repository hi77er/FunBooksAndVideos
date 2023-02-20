using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.DAL.Entities.Enums;
using FunBooksAndVideos.DAL.Entities;
using FunBooksAndVideos.WebApi.Commands.DbCommands;
using FunBooksAndVideos.WebApi.Handlers.Base;
using MediatR;

namespace FunBooksAndVideos.WebApi.Handlers.DbHandlers
{
    public class CommitPurchaseOrderHandler
        : DbHandler, IRequestHandler<CommitPurchaseOrderCommand, Unit>
    {
        public CommitPurchaseOrderHandler(FunDbContext dbContext)
            : base(dbContext) { }

        public async Task<Unit> Handle(CommitPurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            this.AddShippingSlip(request);
            this.AddMemberships(request);

            _dbContext.PurchaseOrders.Add(request.PurchaseOrder);
            await _dbContext.SaveChangesAsync();
            return Unit.Value;
        }

        private void AddShippingSlip(CommitPurchaseOrderCommand request)
        {
            if (request.PurchaseOrder.Items?
                .Any(x => x.Item.ItemType == ItemType.Physical)
                    ?? false)
            {
                request.PurchaseOrder.ShippingSlip = new ShippingSlip()
                {
                    OrderId = request.PurchaseOrder.Id,
                    ShippingCarrier = ShippingCarrier.UPS
                };
            }

            _dbContext.PurchaseOrders.Update(request.PurchaseOrder);
        }

        private void AddMemberships(CommitPurchaseOrderCommand request)
        {
            var customer = _dbContext.Customers
                .First(x => x.Id.Equals(request.PurchaseOrder.CustomerId));

            customer.Memberships = request.PurchaseOrder.Items
                .Where(x => x.Item.SubscriptionType != null)
                .Select(x => new Membership()
                {
                    CustomerId = customer.Id,
                    ItemId = x.Item.Id,
                    EndDate =
                        x.Item.SubscriptionType == SubscriptionType.Monthly
                            ? DateTime.UtcNow.Date.AddMonths(1)
                            :
                                x.Item.SubscriptionType == SubscriptionType.Annual
                                    ? DateTime.UtcNow.Date.AddYears(1)
                                    : DateTime.UtcNow.Date.AddYears(100)

                })
                .ToList();

            _dbContext.Customers.Update(customer);
        }

    }
}
