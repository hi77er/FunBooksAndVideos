using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.DAL.Entities;
using FunBooksAndVideos.DAL.Entities.Enums;
using FunBooksAndVideos.WebApi.Commands.DbCommands;
using FunBooksAndVideos.WebApi.Handlers.Base;
using MediatR;

namespace FunBooksAndVideos.WebApi.Handlers.DbHandlers
{
    public class AddShippingSlipHandler
        : DbHandler, IDbRequestHandler<AddShippingSlipCommand>
    {
        public AddShippingSlipHandler(FunDbContext dbContext)
            : base(dbContext) { }

        public async Task<FunDbContext> Handle(AddShippingSlipCommand request, CancellationToken cancellationToken)
        {
            this.AddShippingSlip(request);
            return _dbContext;
        }

        private void AddShippingSlip(AddShippingSlipCommand request)
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
    }
}
