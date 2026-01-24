using System;

namespace BookNest.Models.ViewModels;

public class MemberDashboardViewModel
{
    public IEnumerable<MemberCheckoutViewModel> ActiveCheckouts { get; set; }
    public IEnumerable<MemberCheckoutViewModel> PastCheckouts { get; set; }
}
