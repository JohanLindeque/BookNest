using Microsoft.AspNetCore.Identity;

namespace BookNest.Models.Entities
{
    public class Checkout
    {
        public int Id { get; set; }
        public int BookId { get; set; } // FK -> Book table
        public Book Book { get; set; }

        public string MemberId { get; set; } // FK -> IdentityUser table
        public IdentityUser Member { get; set; }

        public DateTime CheckoutDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnedDate { get; set; }

        public bool IsReturned => ReturnedDate.HasValue;
        public bool IsOverdue => !IsReturned && DateTime.UtcNow > DueDate;




    }
}
