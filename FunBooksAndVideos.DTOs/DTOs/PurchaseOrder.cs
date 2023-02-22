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
        public ICollection<PurchaseOrderItem>  Items { get; set; } = new List<PurchaseOrderItem>();
        public ShippingSlip? ShippingSlip { get; set; }
    }
}