using BookNest.Constants;
using BookNest.Data;
using BookNest.Models.Entities;
using BookNest.Repositories;
using BookNest.Services;
using BookNest.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookNest.Controllers
{
    public class CheckoutsController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ILibraryService _libraryService;
        private readonly UserManager<IdentityUser> _userManager;

        public CheckoutsController(
            IBookService bookService,
            ILibraryService libraryService,
            UserManager<IdentityUser> userManager
        )
        {
            _bookService = bookService;
            _libraryService = libraryService;
            _userManager = userManager;
        }

        [Authorize(Roles = Roles.Librarian)]
        public async Task<ActionResult> AllCheckouts()
        {
            try
            {
                var allCheckouts = await _libraryService.GetAllCheckouts();

                return View(allCheckouts);
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to load all checkouts.";
                return View(new List<CheckoutListViewModel>());
            }
        }


        [Authorize(Roles = Roles.Member)]
        public async Task<ActionResult> PreviousCheckouts()
        {
            try
            {
                var memberCheckouts = await _libraryService.GetMemberCheckoutHistory(
                    _userManager.GetUserId(User)
                );
                return View(memberCheckouts);
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to load previous checkouts.";
                return View(new List<CheckoutListViewModel>());
            }
        }

        [Authorize(Roles = Roles.Member)]
        public async Task<ActionResult> Checkout(int id)
        {
            try
            {
                if (id == 0)
                    return RedirectToAction("Index", "Books");

                var memeberCheckouts = await _libraryService.GetMemberActiveCheckouts(
                    _userManager.GetUserId(User)
                );

                if (memeberCheckouts.Count() == 5)
                {
                    TempData["Info"] = "You may not checkout more than 5 books at a time.";
                    return RedirectToAction("Index", "Books");
                }

                if (!await _bookService.BookExisits(id))
                {
                    return NotFound();
                }

                await _libraryService.CheckoutBook(id, _userManager.GetUserId(User));

                return RedirectToAction("Index", "Books");
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to cheeckout book. Please try again later.";
                return RedirectToAction("Index", "Books");
            }
        }

        [Authorize(Roles = Roles.Librarian)]
        public async Task<ActionResult> Return(int id)
        {
            try
            {
                await _libraryService.ReturnBook(id);

                return RedirectToAction(nameof(AllCheckouts));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to return book. Please try again later.";
                return RedirectToAction("Index", "Books");
            }
        }
    }
}
