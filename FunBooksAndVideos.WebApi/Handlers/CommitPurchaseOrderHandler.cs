using FunBooksAndVideos.DAL.Entities.Enums;
using FunBooksAndVideos.DAL.Entities;
using MediatR;
using FunBooksAndVideos.Repository.Facade;
using AutoMapper;
using FunBooksAndVideos.WebApi.Handlers.Base;
using FunBooksAndVideos.WebApi.Commands;

namespace FunBooksAndVideos.WebApi.Handlers
{
    public class CommitPurchaseOrderHandler
        : BaseHandler, IRequestHandler<CommitPurchaseOrderCommand, Unit>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;

        public CommitPurchaseOrderHandler(
            ICustomerRepository customerRepository,
            IPurchaseOrderRepository purchaseOrderRepository,
            IMapper mapper)
            : base(mapper)
        {
            this._customerRepository = customerRepository;
            this._purchaseOrderRepository = purchaseOrderRepository;
        }

        public async Task<Unit> Handle(CommitPurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            var entity = this._mapper.Map<PurchaseOrder>(request.PurchaseOrder);

            this._purchaseOrderRepository.Add(entity);

            this.AddShippingSlip(entity);
            this.AddMemberships(entity);

            await this._purchaseOrderRepository.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        private void AddShippingSlip(PurchaseOrder entity)
        {
            if (entity.PurchaseOrderItems?
                .Any(x => x.Item.ItemType == ItemType.Physical)
                    ?? false)
            {
                entity.ShippingSlip = new ShippingSlip()
                {
                    OrderId = entity.Id,
                    ShippingCarrier = ShippingCarrier.UPS
                };

                _purchaseOrderRepository.Update(entity);
            }
        }

        private async void AddMemberships(PurchaseOrder entity)
        {
            var anySubscriptions = entity
                .PurchaseOrderItems
                .Any(x => x.Item.SubscriptionType != null);

            if (anySubscriptions)
            {
                var customer = await this._customerRepository
                    .GetByIdAsync(entity.CustomerId);

                customer.Memberships = entity.PurchaseOrderItems
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

                this._customerRepository.Update(customer);
            }
        }


    }
}
