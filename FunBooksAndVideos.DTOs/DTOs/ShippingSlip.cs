using FunBooksAndVideos.DTOs.Base;
using FunBooksAndVideos.DTOs.Enums;

namespace FunBooksAndVideos.DTOs
{
    public class ShippingSlip : DTOBase
    {
        public ShippingCarrier ShippingCarrier { get; set; }
        public Guid OrderId { get; set; }
        
        public PurchaseOrder Order { get; set; }
    }
}