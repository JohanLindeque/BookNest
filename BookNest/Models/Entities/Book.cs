using System.ComponentModel.DataAnnotations;

namespace BookNest.Models.Entities
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "ISBN is required")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "Publication year is required")]
        public int PublicationYear { get; set; }
        public string Publisher { get; set; } = string.Empty;

        [Required(ErrorMessage = "Author is required")]
        public int AuthorId { get; set; } // FK -> Author table
        public Author Author { get; set; }

        public bool IsAvailable { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Checkout> Checkouts { get; set; } = new List<Checkout>();
    }
}
