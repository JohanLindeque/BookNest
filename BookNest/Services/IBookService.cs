using System;
using BookNest.Models.Entities;

namespace BookNest.Services;

public interface IBookService
{
    Task<IEnumerable<Book>> GetBooksForMember();
    Task<IEnumerable<Book>> GetBooksForLibrarian();
    Task<IEnumerable<Book>> GetBooksByAuthor(int authorId);

    Task<Book> GetBookById(int bookId);
    Task AddNewBook(Book newBook);
    Task UpdateBook(Book book);
    Task DeleteBook(int bookId);
}
