using FunBooksAndVideos.DTOs.Base;
using System.ComponentModel.DataAnnotations;

namespace FunBooksAndVideos.DTOs
{
    public class Customer : DTOBase
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Phone { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        
        public ICollection<PurchaseOrder> Orders { get; set; } = new List<PurchaseOrder>();
        public ICollection<Membership> Memberships { get; set; } = new List<Membership>();
    }
}