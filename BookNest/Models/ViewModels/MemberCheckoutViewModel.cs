using System;

namespace BookNest.Models.ViewModels;

public class MemberCheckoutViewModel
{
    public string BookTitle { get; set; }
    public DateTime CheckoutDate { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsOverdue { get; set; }
}
