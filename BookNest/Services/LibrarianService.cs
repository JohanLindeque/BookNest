using System;
using BookNest.Constants;
using BookNest.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace BookNest.Services;

public class LibrarianService : ILibrarianService
{
    private readonly UserManager<IdentityUser> _userManager;

    public LibrarianService(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> CreateAsync(CreateLibrarianViewModel model)
    {
        var user = new IdentityUser { UserName = model.Email, Email = model.Email };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            return false;

        await _userManager.AddToRoleAsync(user, Roles.Librarian);
        return true;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            return false;

        await _userManager.DeleteAsync(user);
        return true;
    }

    public async Task<List<LibrarianListViewModel>> GetAllAsync()
    {
        var librarians = (await _userManager.GetUsersInRoleAsync(Roles.Librarian))
            .Select(u => new LibrarianListViewModel { Id = u.Id, Email = u.Email })
            .ToList();

        return librarians;
    }
}
