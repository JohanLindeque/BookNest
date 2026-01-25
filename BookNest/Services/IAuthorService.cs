using System;
using BookNest.Models.Entities;

namespace BookNest.Services;

public interface IAuthorService
{
    Task<IEnumerable<Author>> GetAllAuthors();
    Task<Author> GetAuthorById(int authorId);
    Task AddNewAuthor(Author newAuthor);
    Task UpdateAuthor(Author author);
    Task DeleteAuthor(int authorId);
}
