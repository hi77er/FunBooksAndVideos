using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.DAL.Entities;
using FunBooksAndVideos.DAL.Entities.Enums;
using FunBooksAndVideos.WebApi.Commands;
using FunBooksAndVideos.WebApi.Commands.DbCommands;
using FunBooksAndVideos.WebApi.Handlers.Base;
using MediatR;

namespace FunBooksAndVideos.WebApi.Handlers.DbHandlers
{
    public class AddMembershipsHandler
        : DbHandler, IDbRequestHandler<AddMembershipsCommand>
    {
        public AddMembershipsHandler(FunDbContext dbContext)
            : base(dbContext) { }

        public async Task<FunDbContext> Handle(AddMembershipsCommand request, CancellationToken cancellationToken)
        {
            this.AddMemberships(request);
            return _dbContext;
        }

        private void AddMemberships(AddMembershipsCommand request)
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
