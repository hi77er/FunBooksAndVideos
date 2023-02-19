using FunBooksAndVideos.DAL.Entities.Base;

namespace FunBooksAndVideos.DAL.Entities
{
    public class Membership : EntityBase
    {
        public Guid CustomerId { get; set; }
        public Guid ItemId { get; set; }
        public DateTime EndDate { get; set; }
        
        public Item Item { get; set; }
    }
}