using FunBooksAndVideos.DTOs.Base;
using System.ComponentModel.DataAnnotations;

namespace FunBooksAndVideos.DTOs
{
    public class PurchaseOrder : DTOBase
    {
        [Required]
        public Guid CustomerId { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        
        public Customer Customer { get; set; }
        public ICollection<PurchaseOrderItem>  PurchaseOrderItems { get; set; }
        public ShippingSlip? ShippingSlip { get; set; }
    }
}