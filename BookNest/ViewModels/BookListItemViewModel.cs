using System;

namespace BookNest.ViewModels;

public class BookListItemViewModel
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string ISBN { get; set; }
    public int PublicationYear { get; set; }

    public string AuthorName { get; set; } = string.Empty;

    public bool IsAvailable { get; set; }
}
