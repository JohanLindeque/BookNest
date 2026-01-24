namespace BookNest.Models.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ISBN { get; set; }
        public int PublicationYear { get; set; }
        public string Publisher { get; set; } = string.Empty;

        public int AuthorId { get; set; } // FK -> Author table
        public Author Author { get; set; }

        public bool IsAvailable { get; set; } = true;
        public DateTime CreatedAt { get; set; }

        public ICollection<Checkout> Checkouts { get; set; } = new List<Checkout>();
    }
}
