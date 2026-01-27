using System;
using System.Reflection.Metadata.Ecma335;
using BookNest.Models.Entities;
using BookNest.Repositories;
using SQLitePCL;

namespace BookNest.Services;

public class LibraryService : ILibraryService
{
    private readonly ICheckoutRepository _checkoutRepo;
    private readonly IBookService _bookService;

    public LibraryService(ICheckoutRepository checkoutRepo, IBookService bookService)
    {
        _checkoutRepo = checkoutRepo;
        _bookService = bookService;
    }

    public async Task CheckoutBook(int bookId, string memberId)
    {
        var checkout = new Checkout { BookId = bookId, MemberId = memberId };

        await _checkoutRepo.AddAsync(checkout);

        var book = await _bookService.GetBookById(bookId);

        book.IsAvailable = false;

        await _bookService.UpdateBook(book);
    }

    public async Task<IEnumerable<Checkout>> GetAllCheckouts()
    {
        var allCheckouts = await _checkoutRepo.GetAllAsync();

        return allCheckouts;
    }

    public async Task<IEnumerable<Checkout>> GetMemberActiveCheckouts(string memberId)
    {
        var allCheckouts = await _checkoutRepo.GetByUserIdAsync(memberId);

        var activeCheckouts = allCheckouts.Where(chk => chk.IsReturned == false).ToList();

        return activeCheckouts;
    }

    public async Task<IEnumerable<Checkout>> GetMemberCheckoutHistory(string memberId)
    {
        var allCheckouts = await _checkoutRepo.GetByUserIdAsync(memberId);

        var previousCheckouts = allCheckouts.Where(chk => chk.IsReturned == true).ToList();
        return previousCheckouts;
    }

    public async Task<IEnumerable<Checkout>> GetOverdueCheckouts()
    {
        var overdueCheckouts = await _checkoutRepo.GetAllOverdueAsync();

        return overdueCheckouts;
    }

    public async Task ReturnBook(int checkoutId)
    {
        var checkout = await _checkoutRepo.GetByIdAsync(checkoutId);

        if (checkout == null)
        {
            throw new KeyNotFoundException();
        }

        checkout.ReturnedDate = DateTime.Now;

        await _checkoutRepo.UpdateAsync(checkout);

        var book = await _bookService.GetBookById(checkout.BookId);

        book.IsAvailable = true;

        await _bookService.UpdateBook(book);
    }
}
