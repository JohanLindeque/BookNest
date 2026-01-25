using System;
using System.Reflection.Metadata.Ecma335;
using BookNest.Models.Entities;
using BookNest.Repositories;

namespace BookNest.Services;

public class LibraryService : ILibraryService
{
    private readonly BookRepository _bookRepo;

    public LibraryService(BookRepository bookRepo)
    {
        _bookRepo = bookRepo;
    }

    public void CheckoutBook(int bookId, string memberId)
    {
        throw new NotImplementedException();
    }

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
