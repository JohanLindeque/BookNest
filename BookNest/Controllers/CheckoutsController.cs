using BookNest.Constants;
using BookNest.Data;
using BookNest.Models.Entities;
using BookNest.Repositories;
using BookNest.Services;
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
            // TODO: return all checkouts
            var allCheckouts = await _libraryService.GetAllCheckouts();

            return View(allCheckouts);
        }

        [Authorize(Roles = Roles.Librarian)]
        public async Task<ActionResult> OverdueCheckouts()
        {
            // TODO: return all checkouts
            var overdueCheckouts = await _libraryService.GetOverdueCheckouts();

            return View(overdueCheckouts);
        }

        [Authorize(Roles = Roles.Member)]
        public async Task<ActionResult> PreviousCheckouts()
        {
            var memberCheckouts = await _libraryService.GetMemberCheckoutHistory(
                _userManager.GetUserId(User)
            );
            return View(memberCheckouts);
        }

        [Authorize(Roles = Roles.Member)]
        public async Task<ActionResult> ActiveCheckouts()
        {
            var memberCheckouts = await _libraryService.GetMemberActiveCheckouts(
                _userManager.GetUserId(User)
            );
            return View(memberCheckouts);
        }

        [Authorize(Roles = Roles.Member)]
        public async Task<ActionResult> Checkout(int id)
        {
            // TODO: Checkout book for memeber
            // TODO: check business roles

            if (id == 0)
                return RedirectToAction("Index", "Books");

            var memeberCheckouts = await _libraryService.GetMemberActiveCheckouts(
                _userManager.GetUserId(User)
            );

            if (memeberCheckouts.Count() == 5)
            {
                // TODO message cant checkout book
                return RedirectToAction("Index", "Books");
            }

            if (!await _bookService.BookExisits(id))
            {
                return NotFound();
            }

            await _libraryService.CheckoutBook(id, _userManager.GetUserId(User));

            return RedirectToAction("Index", "Books");
        }

        [Authorize(Roles = Roles.Librarian)]
        public async Task<ActionResult> Return(int id)
        {
            await _libraryService.ReturnBook(id);

            return RedirectToAction("Index", "Books");
        }
    }
}
