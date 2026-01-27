using System;
using BookNest.Data;
using BookNest.Models.Entities;
using BookNest.Repositories;
using BookNest.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
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
        await _authorRepo.AddAsync(newAuthor);
    }

    public async Task<IEnumerable<AuthorDropdownViewModel>> BuildAuthorDropDownList()
    {
        var authors = await GetAllAuthors();
        var dropDownList = new List<AuthorDropdownViewModel>();

        if (authors.Any())
        {
            foreach (var author in authors)
            {
                dropDownList.Add(
                    new AuthorDropdownViewModel
                    {
                        Id = author.Id,
                        FullName = author.FirstName + " " + author.LastName,
                    }
                );
            }
        }

        return dropDownList;
    }

    public async Task DeleteAuthor(int authorId)
    {
        await _authorRepo.DeleteAsync(authorId);
    }

    public async Task<IEnumerable<Author>> GetAllAuthors()
    {
        var authors = await _authorRepo.GetAllAsync();

        return authors;
    }

    public async Task<Author> GetAuthorById(int authorId)
    {
        var author = await _authorRepo.GetByIdAsync(authorId);

        return author;
    }

    public async Task UpdateAuthor(Author author)
    {
        await _authorRepo.UpdateAsync(author);
    }
}
