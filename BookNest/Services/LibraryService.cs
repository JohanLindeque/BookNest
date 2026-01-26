using System;
using System.Reflection.Metadata.Ecma335;
using BookNest.Models.Entities;
using BookNest.Repositories;

namespace BookNest.Services;

public class LibraryService : ILibraryService
{
    private readonly IRepository<Checkout> _checkoutRepo;
    private readonly IBookService _bookService;

    public LibraryService(IRepository<Checkout> checkoutRepo, IBookService bookService)
    {
        _checkoutRepo = checkoutRepo;
        _bookService = bookService;
    }

    public void CheckoutBook(int bookId, string memberId) { }

    public Task<IEnumerable<Checkout>> GetAllCheckouts()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Checkout>> GetMemberActiveCheckouts(string memberId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Checkout>> GetMemberCheckoutHistory(string memberId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Checkout>> GetOverdueCheckouts()
    {
        throw new NotImplementedException();
    }

    public void ReturnBook(int checkoutId)
    {
        throw new NotImplementedException();
    }
}
