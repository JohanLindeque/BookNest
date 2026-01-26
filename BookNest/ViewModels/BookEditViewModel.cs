using System;
using System.ComponentModel.DataAnnotations;

namespace BookNest.ViewModels;

public class BookEditViewModel
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string ISBN { get; set; }

    public int PublicationYear { get; set; }

    public string Publisher { get; set; } = string.Empty;

    [Required]
    public int AuthorId { get; set; }

    public bool IsAvailable { get; set; }

    public IEnumerable<AuthorDropdownViewModel> Authors { get; set; } =
        new List<AuthorDropdownViewModel>();
}
