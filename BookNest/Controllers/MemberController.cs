using BookNest.Constants;
using BookNest.Data;
using BookNest.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace BookNest.Controllers
{
    public class MemberController : Controller
    {
        private readonly ILibraryService _libraryService;
        private readonly UserManager<IdentityUser> _userManager;

        public MemberController(
            ILibraryService libraryService,
            UserManager<IdentityUser> userManager
        )
        {
            _libraryService = libraryService;
            _userManager = userManager;
        }

        [Authorize(Roles = Roles.Member)]
        public async Task<ActionResult> Dashboard()
        {
            try
            {
                var overdue = await _libraryService.GetMemberActiveCheckouts(
                    _userManager.GetUserId(User)
                );
                return View(overdue);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to load active checkouts.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
