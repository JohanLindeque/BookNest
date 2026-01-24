namespace BookNest.Models.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int AuthorId { get; set; } // FK -> Author table
        public Author Author { get; set; }
        public bool IsAvailable { get; set; } = true;

        public ICollection<Checkout> Checkouts { get; set; } = new List<Checkout>();

    }
}
