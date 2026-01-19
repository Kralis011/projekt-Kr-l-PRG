using System.Diagnostics;
using Kral_InvApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kral_InvApp.Controllers
{
    using Kral_InvApp.Data;
    using Kral_InvApp.Entities;

    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var user = _context.Users.FirstOrDefault(u => u.user_id == userId);

            return View(user);
        }
    }

}
