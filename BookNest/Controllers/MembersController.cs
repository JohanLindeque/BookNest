using BookNest.Constants;
using BookNest.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookNest.Controllers
{
    public class MembersController : Controller
    {
        private readonly AppDbContext _context;

        public MembersController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = Roles.Member)]
        public ActionResult Dashboard()
        {
            // TODO: retrieve Current & past checkouts for memeber
            return View();
        }
    }
}
