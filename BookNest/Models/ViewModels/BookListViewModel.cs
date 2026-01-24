using System;

namespace BookNest.Models.ViewModels;

public class BookListViewModel
{
    public IEnumerable<BookListItemViewModel> Books { get; set; }
    public int? SelectedAuthorId { get; set; }
    public IEnumerable<SelectListItem> Authors { get; set; }
}
