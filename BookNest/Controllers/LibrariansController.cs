using BookNest.Constants;
using BookNest.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookNest.Controllers
{
    public class LibrariansController : Controller
    {
   

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
