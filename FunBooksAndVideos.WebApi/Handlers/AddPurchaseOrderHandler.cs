
using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.DAL.Entities;
using FunBooksAndVideos.DAL.Entities.Enums;
using FunBooksAndVideos.WebApi.Commands;
using MediatR;

namespace FunBooksAndVideos.WebApi.Handlers
{
    public class AddPurchaseOrderHandler : IRequestHandler<AddPurchaseOrderCommand, Unit>
    {
        private readonly FunDbContext _dbContext;
        public AddPurchaseOrderHandler(FunDbContext dbContext) => _dbContext = dbContext;

        public async Task<Unit> Handle(AddPurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            this.AddShippingSlip(request);
            this.AddMemberships(request);

            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        private void AddMemberships(AddPurchaseOrderCommand request)
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
                            : (
                                x.Item.SubscriptionType == SubscriptionType.Annual
                                    ? DateTime.UtcNow.Date.AddYears(1)
                                    : DateTime.UtcNow.Date.AddYears(100)
                            )
                })
                .ToList();

            _dbContext.Customers.Update(customer);
        }

        private void AddShippingSlip(AddPurchaseOrderCommand request)
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

            _dbContext.PurchaseOrders.Add(request.PurchaseOrder);
        }
    }
}
