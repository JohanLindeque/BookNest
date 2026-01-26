using System;
using BookNest.Models.Entities;

namespace BookNest.Services;

public interface ILibraryService
{
    void CheckoutBook(int bookId, string memberId);
    void ReturnBook(int checkoutId);

    Task<IEnumerable<Checkout>> GetMemberActiveCheckouts(string memberId);
    Task<IEnumerable<Checkout>> GetMemberCheckoutHistory(string memberId);

    Task<IEnumerable<Checkout>> GetAllCheckouts();
    Task<IEnumerable<Checkout>> GetOverdueCheckouts();
}
