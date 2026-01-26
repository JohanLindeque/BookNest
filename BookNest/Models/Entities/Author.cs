using System.ComponentModel.DataAnnotations;

namespace BookNest.Models.Entities
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Biography { get; set; }
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
