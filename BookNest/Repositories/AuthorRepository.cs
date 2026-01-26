using System;
using BookNest.Data;
using BookNest.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookNest.Repositories;

public class AuthorRepository : IRepository<Author>
{
    private readonly AppDbContext _context;

    public AuthorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Author entity)
    {
        await _context.Authors.AddAsync(entity);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var authorEntry = await _context.Authors.FirstOrDefaultAsync(author => author.Id == id);

        if (authorEntry == null)
            throw new KeyNotFoundException();

        _context.Authors.Remove(authorEntry);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Author>> GetAllAsync()
    {
        var authorsInDb = await _context.Authors.ToListAsync();

        return authorsInDb;
    }

    public async Task<Author> GetByIdAsync(int id)
    {
        var authorEntry = await _context.Authors.FirstOrDefaultAsync(author => author.Id == id);

        if (authorEntry == null)
            throw new KeyNotFoundException();

        return authorEntry;
    }

    public async Task UpdateAsync(Author entity)
    {
        _context.Authors.Update(entity);

        await _context.SaveChangesAsync();
    }
}
