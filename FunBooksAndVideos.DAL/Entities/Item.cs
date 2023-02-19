using FunBooksAndVideos.DAL.Entities.Base;
using FunBooksAndVideos.DAL.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace FunBooksAndVideos.DAL.Entities
{
    public class Item : EntityBase
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
