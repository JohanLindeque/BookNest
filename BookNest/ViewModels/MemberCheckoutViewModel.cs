using System;

namespace BookNest.ViewModels;

public class MemberCheckoutViewModel
{
    public int CheckoutId { get; set; }

    public string BookTitle { get; set; } = string.Empty;

    public DateTime CheckoutDate { get; set; }
    public DateTime DueDate { get; set; }

    public bool IsOverdue { get; set; }
    
}
