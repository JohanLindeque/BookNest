using System.Diagnostics;
using BookNest.Constants;
using BookNest.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookNest.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        // not logged in, show landing page
        if (!User.Identity?.IsAuthenticated ?? true)
        {
            return View();
        }

        // Logged in + Member role
        if (User.IsInRole(Roles.Member))
        {
            return RedirectToAction("Dashboard", "Member");
        }

        // Logged in + Librarian role
        if (User.IsInRole(Roles.Librarian))
        {
            return RedirectToAction("Dashboard", "Librarian");
        }

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(
            new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }
        );
    }
}
