using FunBooksAndVideos.DAL.Entities.Base;
using FunBooksAndVideos.DAL.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace FunBooksAndVideos.DAL.Entities
{
    public class ItemAttribute : EntityBase
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public AttributeType AttributeType { get; set; }
        [Required]
        public string Value { get; set; } = string.Empty;
        [Required]
        public Guid ItemId { get; set; }

        public Item Item { get; set; }

    }
}
