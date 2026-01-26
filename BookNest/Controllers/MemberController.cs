using BookNest.Constants;
using BookNest.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookNest.Controllers
{
    public class MemberController : Controller
    {
        [Authorize(Roles = Roles.Member)]
        public ActionResult Dashboard()
        {
            // TODO: retrieve Current & past checkouts for memeber
            return View();
        }
    }
}
