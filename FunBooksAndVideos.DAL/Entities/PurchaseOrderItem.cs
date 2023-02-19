using FunBooksAndVideos.DAL.Entities.Enums;

namespace FunBooksAndVideos.DAL.Entities
{
    public class PurchaseOrderItem
    {
        public Guid OrderId { get; set; }
        public Guid ItemId { get; set; }

        public int? Amount { get; set; }

        public PurchaseOrder Order { get; set; }
        public Item Item { get; set; }
    }
}
