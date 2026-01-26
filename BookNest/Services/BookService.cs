using System;
using BookNest.Models.Entities;
using BookNest.Repositories;
using BookNest.ViewModels;

namespace BookNest.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepo;
    private readonly IAuthorService _authorService;

    public BookService(IBookRepository bookRepo, IAuthorService authorService)
    {
        _bookRepo = bookRepo;
        _authorService = authorService;
    }

    public async Task AddNewBook(BookCreateViewModel bookVm)
    {
        try
        {
            var newBook = new Book
            {
                Title = bookVm.Title,
                Description = bookVm.Description,
                ISBN = bookVm.ISBN,
                PublicationYear = bookVm.PublicationYear,
                Publisher = bookVm.Publisher,
                AuthorId = bookVm.AuthorId,
            };

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

    public async Task UpdateBook(BookEditViewModel bookVm)
    {
        try
        {
            var book = new Book
            {
                Id = bookVm.Id,
                Title = bookVm.Title,
                Description = bookVm.Description,
                ISBN = bookVm.ISBN,
                PublicationYear = bookVm.PublicationYear,
                Publisher = bookVm.Publisher,
                AuthorId = bookVm.AuthorId,
                IsAvailable = bookVm.IsAvailable,
                CreatedAt = bookVm.CreatedAt,
            };

            await _bookRepo.UpdateAsync(book);
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public async Task<BookEditViewModel> BuildEditViewModel(Book book)
    {
        var bookVm = new BookEditViewModel
        {
            Id = book.Id,
            Title = book.Title,
            Description = book.Description,
            ISBN = book.ISBN,
            PublicationYear = book.PublicationYear,
            Publisher = book.Publisher,
            AuthorId = book.AuthorId,
            IsAvailable = book.IsAvailable,
            CreatedAt = book.CreatedAt,
            Authors = await _authorService.BuildAuthorDropDownList(),
        };

        return bookVm;
    }
}
