using BookNest.Constants;
using BookNest.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookNest.Controllers
{
    public class CheckoutsController : Controller
    {
        private readonly AppDbContext _context;

        public CheckoutsController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = Roles.Librarian)]
        public ActionResult AllCheckouts()
        {
            // TODO: return all checkouts
            return View();
        }

        [Authorize(Roles = Roles.Member)]
        public ActionResult MyCheckouts()
        {
            // TODO: return all checkouts for memeber
            return View();
        }

        [Authorize(Roles = Roles.Member)]
        public ActionResult Checkout(int bookId)
        {
            // TODO: Checkout book for memeber
            // TODO: check business roles

            return View();
        }

        [Authorize(Roles = Roles.Librarian)]
        public ActionResult Return(int checkoutId)
        {
            // TODO: return book for memeber
            // TODO: check business roles

            return View();
        }
    }
}
