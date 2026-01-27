using System;
using System.ComponentModel.DataAnnotations;

namespace BookNest.ViewModels;

public class BookCreateViewModel
{
    [Required]
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [Required]
    public string ISBN { get; set; }

    [Required]
    public int PublicationYear { get; set; }

    public string Publisher { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Author")]
    public int AuthorId { get; set; }

    // For dropdown
    public IEnumerable<AuthorDropdownViewModel> Authors { get; set; } =
        new List<AuthorDropdownViewModel>();
}
