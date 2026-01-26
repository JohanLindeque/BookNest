using System;

namespace BookNest.ViewModels;

public class CheckoutListViewModel
{
    public int Id { get; set; }

    public string BookTitle { get; set; } = string.Empty;
    public string MemberEmail { get; set; } = string.Empty;

    public DateTime CheckoutDate { get; set; }
    public DateTime DueDate { get; set; }

    public bool IsReturned { get; set; }
    public bool IsOverdue { get; set; }
}
