using System;
using BookNest.Data;
using BookNest.Models.Entities;
using BookNest.Repositories;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace BookNest.Services;

public class AuthorService : IAuthorService
{
    private readonly IRepository<Author> _authorRepo;

    public AuthorService(IRepository<Author> authRepo)
    {
        _authorRepo = authRepo;
    }

    public async Task AddNewAuthor(Author newAuthor)
    {
        try
        {
            await _authorRepo.AddAsync(newAuthor);
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public async Task DeleteAuthor(int authorId)
    {
        try
        {
            await _authorRepo.DeleteAsync(authorId);
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<Author>> GetAllAuthors()
    {
        try
        {
            var authors = await _authorRepo.GetAllAsync();

            return authors;
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public async Task<Author> GetAuthorById(int authorId)
    {
        try
        {
            var author = await _authorRepo.GetByIdAsync(authorId);

            return author;
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public async Task UpdateAuthor(Author author)
    {
        try
        {
            await _authorRepo.UpdateAsync(author);
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}
