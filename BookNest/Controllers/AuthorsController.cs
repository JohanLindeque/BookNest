using Microsoft.AspNetCore.Mvc;

namespace BookNest.Controllers
{
    public class AuthorsController : Controller
    {
        // GET: AuthorsController
        public ActionResult Index()
        {
            return View();
        }

    }
}
