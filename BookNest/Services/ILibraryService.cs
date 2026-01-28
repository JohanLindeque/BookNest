using System;
using BookNest.Models.Entities;
using BookNest.ViewModels;

namespace BookNest.Services;

public interface ILibraryService
{
    Task CheckoutBook(int bookId, string memberId);
    Task ReturnBook(int checkoutId);

    Task<IEnumerable<CheckoutListViewModel>> GetMemberActiveCheckouts(string memberId);
    Task<IEnumerable<CheckoutListViewModel>> GetMemberCheckoutHistory(string memberId);
    Task<IEnumerable<CheckoutListViewModel>> GetMemberOverdueCheckouts(string memberId);

    Task<IEnumerable<CheckoutListViewModel>> GetAllCheckouts();
    Task<IEnumerable<CheckoutListViewModel>> GetOverdueCheckouts();
}
