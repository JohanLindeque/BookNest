using BookNest.Constants;
using BookNest.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookNest.Controllers
{
    public class LibrariansController : Controller
    {
        private readonly AppDbContext _context;

        public LibrariansController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = Roles.Librarian)]
        public ActionResult Dashboard()
        {
            // TODO: retrieve checkouts for all memebers
            return View();
        }

        [Authorize(Roles = Roles.Librarian)]
        public ActionResult Overdue()
        {
            // TODO: retrieve checkouts for all memebers
            return View();
        }
    }
}
