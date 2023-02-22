using System.ComponentModel.DataAnnotations;

namespace FunBooksAndVideos.DTOs.Base
{
    public abstract class DTOBase
    {
        [Required]
        public Guid Id { get; set; }

    }
}
