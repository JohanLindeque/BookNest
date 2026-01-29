using System;

namespace BookNest.ViewModels;

public class MemberListViewModel
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public int  ActiveCheckoutsCount { get; set; } = 0;
    public int  OverdueCheckoutsCount { get; set; } = 0;

    
}
