using System;
using BookNest.Models.Entities;

namespace BookNest.Repositories;

public interface IBookRepository : IRepository<Book>
{
    Task<IEnumerable<Book>> GetAvailableAsync();
    Task<IEnumerable<Book>> GetByAuthorIdAsync(int authorId);
}
