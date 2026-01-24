using System;

namespace BookNest.Models.ViewModels;

public class LibrarianDashboardViewModel
{
    public IEnumerable<MemberCheckoutViewModel> OverdueCheckouts { get; set; }
}
