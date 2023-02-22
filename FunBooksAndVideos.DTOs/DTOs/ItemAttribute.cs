using FunBooksAndVideos.DTOs.Base;
using FunBooksAndVideos.DTOs.Enums;
using System.ComponentModel.DataAnnotations;

namespace FunBooksAndVideos.DTOs
{
    public class ItemAttribute : DTOBase
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
