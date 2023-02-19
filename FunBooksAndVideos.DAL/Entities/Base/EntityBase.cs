using System.ComponentModel.DataAnnotations;

namespace FunBooksAndVideos.DAL.Entities.Base
{
    public abstract class EntityBase
    {
        [Key]
        public Guid Id { get; set; }

    }
}
