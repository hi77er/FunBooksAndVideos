using FunBooksAndVideos.DTOs.Base;
using FunBooksAndVideos.DTOs.Enums;
using System.ComponentModel.DataAnnotations;

namespace FunBooksAndVideos.DTOs
{
    public class Item : DTOBase
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; }

        [Required]
        public ItemType ItemType { get; set; }
        public SubscriptionType? SubscriptionType { get; set; }

        public ICollection<ItemAttribute> Attributes { get; set; }

    }
}
