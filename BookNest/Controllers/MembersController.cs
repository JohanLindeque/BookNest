using Microsoft.AspNetCore.Mvc;

namespace BookNest.Controllers
{
    public class MembersController : Controller
    {
        // GET: MembersController
        public ActionResult Index()
        {
            return View();
        }

    }
}
