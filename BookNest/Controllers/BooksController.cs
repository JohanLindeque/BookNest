using Microsoft.AspNetCore.Mvc;

namespace BookNest.Controllers
{
    public class BooksController : Controller
    {
        // GET: BooksController
        public ActionResult Index()
        {
            return View();
        }

    }
}
