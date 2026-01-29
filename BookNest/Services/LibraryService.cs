using System;
using System.Reflection.Metadata.Ecma335;
using BookNest.Constants;
using BookNest.Models.Entities;
using BookNest.Repositories;
using BookNest.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using SQLitePCL;

namespace BookNest.Services;

public class LibraryService : ILibraryService
{
    private readonly ICheckoutRepository _checkoutRepo;
    private readonly IBookService _bookService;
    private readonly UserManager<IdentityUser> _userManager;

    public LibraryService(
        ICheckoutRepository checkoutRepo,
        IBookService bookService,
        UserManager<IdentityUser> userManager
    )
    {
        _checkoutRepo = checkoutRepo;
        _bookService = bookService;
        _userManager = userManager;
    }

    public async Task CheckoutBook(int bookId, string memberId)
    {
        var checkout = new Checkout { BookId = bookId, MemberId = memberId };

        await _checkoutRepo.AddAsync(checkout);

        var book = await _bookService.GetBookById(bookId);

        book.IsAvailable = false;

        await _bookService.UpdateBook(book);
    }

    public async Task<IEnumerable<CheckoutListViewModel>> GetAllCheckouts()
    {
        var allCheckouts = await _checkoutRepo.GetAllAsync();

        var checkouts = allCheckouts.ToList();

        return BuildCheckoutVmList(checkouts);
    }

    public async Task<IEnumerable<CheckoutListViewModel>> GetMemberActiveCheckouts(string memberId)
    {
        var allCheckouts = await _checkoutRepo.GetByUserIdAsync(memberId);

        var activeCheckouts = allCheckouts.Where(chk => chk.IsReturned == false).ToList();

        return BuildCheckoutVmList(activeCheckouts);
    }

    public async Task<IEnumerable<CheckoutListViewModel>> GetMemberCheckoutHistory(string memberId)
    {
        var allCheckouts = await _checkoutRepo.GetByUserIdAsync(memberId);

        var previousCheckouts = allCheckouts.Where(chk => chk.IsReturned == true).ToList();

        return BuildCheckoutVmList(previousCheckouts);
    }

    public async Task<IEnumerable<CheckoutListViewModel>> GetMemberOverdueCheckouts(string memberId)
    {
        var allCheckouts = await _checkoutRepo.GetByUserIdAsync(memberId);

        var overdueCheckouts = allCheckouts
            .Where(chk => chk.IsReturned == false && chk.IsOverdue == true)
            .ToList();

        return BuildCheckoutVmList(overdueCheckouts);
    }

    public async Task<IEnumerable<CheckoutListViewModel>> GetOverdueCheckouts()
    {
        var overdueCheckouts = await _checkoutRepo.GetAllOverdueAsync();

        var checkouts = overdueCheckouts.ToList();

        return BuildCheckoutVmList(checkouts);
    }

    public async Task ReturnBook(int checkoutId)
    {
        var checkout = await _checkoutRepo.GetByIdAsync(checkoutId);

        checkout.ReturnedDate = DateTime.Now;

        await _checkoutRepo.UpdateAsync(checkout);

        var book = await _bookService.GetBookById(checkout.BookId);

        book.IsAvailable = true;

        await _bookService.UpdateBook(book);
    }

    private static IEnumerable<CheckoutListViewModel> BuildCheckoutVmList(
        List<Checkout> checkoutsInDb
    )
    {
        var checkoutVmList = new List<CheckoutListViewModel>();

        foreach (var checkout in checkoutsInDb)
        {
            var checkoutVm = new CheckoutListViewModel
            {
                Id = checkout.Id,
                BookTitle = checkout.Book.Title,
                MemberEmail = checkout.Member.Email,
                CheckoutDate = checkout.CheckoutDate,
                DueDate = checkout.DueDate,
                IsReturned = checkout.IsReturned,
                IsOverdue = checkout.IsOverdue,
            };

            checkoutVmList.Add(checkoutVm);
        }

        return checkoutVmList;
    }

    public async Task<IEnumerable<MemberListViewModel>> GetMembersInfo()
    {
        var members = await _userManager.GetUsersInRoleAsync(Roles.Member);

        var memberList = members
            .Select(u => new MemberListViewModel { UserId = u.Id, UserName = u.UserName })
            .ToList();

        foreach (var member in memberList)
        {
            var activeCheckouts = await GetMemberActiveCheckouts(member.UserId);
            var overdueCheckouts = await GetMemberOverdueCheckouts(member.UserId);

            member.ActiveCheckoutsCount = activeCheckouts.Count();
            member.OverdueCheckoutsCount = overdueCheckouts.Count();
        }

        return memberList;
    }
}
