using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BookNest.Models.Entities
{
    public class Checkout
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int BookId { get; set; } // FK -> Book table
        public Book Book { get; set; }

        [Required]
        public string MemberId { get; set; } // FK -> IdentityUser table
        public IdentityUser Member { get; set; }

        [Required]
        public DateTime CheckoutDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }
        public DateTime? ReturnedDate { get; set; }

        public bool IsReturned => ReturnedDate.HasValue;
        public bool IsOverdue => !IsReturned && DateTime.UtcNow > DueDate;
    }
}
