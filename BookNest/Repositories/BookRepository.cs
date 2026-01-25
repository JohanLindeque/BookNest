using System;
using BookNest.Data;
using BookNest.Models.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BookNest.Repositories;

public class BookRepository : IRepository<Book>
{
    private readonly AppDbContext _context;

    public BookRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Book entity)
    {
        await _context.Books.AddAsync(entity);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var bookEntry = await _context.Books.FirstOrDefaultAsync(book => book.Id == id);

        if (bookEntry == null)
            throw new KeyNotFoundException();

        _context.Books.Remove(bookEntry);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        var booksInDb = await _context.Books.ToListAsync();

        return booksInDb;
    }

    public async Task<Book> GetByIdAsync(int id)
    {
        var bookEntry = await _context.Books.FirstOrDefaultAsync(book => book.Id == id);

        if (bookEntry == null)
            throw new KeyNotFoundException();

        return bookEntry;
    }

    public async Task UpdateAsync(Book entity)
    {
        _context.Books.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Book>> GetAvailableAsync()
    {
        var availableBooks = await _context
            .Books.Include(book => book.Author)
            .Where(book => book.IsAvailable == true)
            .ToListAsync();

        return availableBooks;
    }

    public async Task<IEnumerable<Book>> GetByAuthorIdAsync(int authorId)
    {
        var authorBooks = await _context
            .Books.Include(book => book.Author)
            .Where(book => book.AuthorId == authorId)
            .ToListAsync();

        return authorBooks;
    }
}
