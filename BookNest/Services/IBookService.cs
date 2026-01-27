using System;
using BookNest.Models.Entities;
using BookNest.ViewModels;

namespace BookNest.Services;

public interface IBookService
{
    Task<IEnumerable<BookListItemViewModel>> GetBooksForMember();
    Task<IEnumerable<BookListItemViewModel>> GetBooksForLibrarian();
    Task<IEnumerable<Book>> GetBooksByAuthor(int authorId);

    Task<Book> GetBookById(int bookId);
    Task AddNewBook(BookCreateViewModel bookVm);
    Task UpdateBook(BookEditViewModel bookVm);
    Task UpdateBook(Book book);
    Task DeleteBook(int bookId);
    Task <BookEditViewModel> BuildEditViewModel(Book book);
    Task<bool> BookExisits(int bookId);

}
