using System;
using BookNest.Models.Entities;
using BookNest.Repositories;

namespace BookNest.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepo;

    public BookService(IBookRepository bookRepo)
    {
        _bookRepo = bookRepo;
    }

    public async Task AddNewBook(Book newBook)
    {
        try
        {
            await _bookRepo.AddAsync(newBook);
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public async Task<Book> GetBookById(int bookId)
    {
        try
        {
            var book = await _bookRepo.GetByIdAsync(bookId);

            return book;
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<Book>> GetBooksByAuthor(int authorId)
    {
        try
        {
            var booksByAuthor = await _bookRepo.GetByAuthorIdAsync(authorId);

            return booksByAuthor;
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<Book>> GetBooksForLibrarian()
    {
        try
        {
            var availableBooks = await _bookRepo.GetAllAsync();

            return availableBooks;
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<Book>> GetBooksForMember()
    {
        try
        {
            var availableBooks = await _bookRepo.GetAvailableAsync();

            return availableBooks;
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public async Task DeleteBook(int bookId)
    {
        try
        {
            await _bookRepo.DeleteAsync(bookId);
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public async Task UpdateBook(Book book)
    {
        try
        {
            await _bookRepo.UpdateAsync(book);
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}
