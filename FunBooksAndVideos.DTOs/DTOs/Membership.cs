using FunBooksAndVideos.DTOs.Base;

namespace FunBooksAndVideos.DTOs
{
    public class Membership : DTOBase
    {
        public Guid CustomerId { get; set; }
        public Guid ItemId { get; set; }
        public DateTime EndDate { get; set; }
        
        public Item Item { get; set; }
    }
}