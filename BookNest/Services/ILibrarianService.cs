using System;
using BookNest.ViewModels;

namespace BookNest.Services;

public interface ILibrarianService
{
    Task<List<LibrarianListViewModel>> GetAllAsync();
    Task<bool> CreateAsync(CreateLibrarianViewModel model);
    Task<bool> DeleteAsync(string id);
}
