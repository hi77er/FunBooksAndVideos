using FunBooksAndVideos.DAL.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace FunBooksAndVideos.DAL.Entities
{
    public class PurchaseOrder : EntityBase
    {
        [Required]
        public Guid CustomerId { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        
        public Customer Customer { get; set; }
        public ICollection<PurchaseOrderItem>  Items { get; set; } = new List<PurchaseOrderItem>();
        public ShippingSlip? ShippingSlip { get; set; }
    }
}