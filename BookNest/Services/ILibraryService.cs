using System;
using BookNest.Models.Entities;

namespace BookNest.Services;

public interface ILibraryService
{
    Task CheckoutBook(int bookId, string memberId);
    Task ReturnBook(int checkoutId);

    Task<IEnumerable<Checkout>> GetMemberActiveCheckouts(string memberId);
    Task<IEnumerable<Checkout>> GetMemberCheckoutHistory(string memberId);
    Task<IEnumerable<Checkout>> GetMemberOverdueCheckouts(string memberId);

    Task<IEnumerable<Checkout>> GetAllCheckouts();
    Task<IEnumerable<Checkout>> GetOverdueCheckouts();
}
