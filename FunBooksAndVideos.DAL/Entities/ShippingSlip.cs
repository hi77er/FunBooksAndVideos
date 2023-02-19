using FunBooksAndVideos.DAL.Entities.Base;
using FunBooksAndVideos.DAL.Entities.Enums;

namespace FunBooksAndVideos.DAL.Entities
{
    public class ShippingSlip : EntityBase
    {
        public ShippingCarrier ShippingCarrier { get; set; }
        public Guid OrderId { get; set; }
        
        public PurchaseOrder Order { get; set; }
    }
}