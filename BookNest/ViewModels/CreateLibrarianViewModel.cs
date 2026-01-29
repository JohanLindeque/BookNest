using System;
using System.ComponentModel.DataAnnotations;

namespace BookNest.ViewModels;

public class CreateLibrarianViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [MinLength(6)]
    public string Password { get; set; }
}
