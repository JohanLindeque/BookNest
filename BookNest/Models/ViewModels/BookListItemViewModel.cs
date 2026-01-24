using System;

namespace BookNest.Models.ViewModels;

public class BookListItemViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string AuthorName { get; set; }
    public bool IsAvailable { get; set; }
}
