using Kral_InvApp.Data;
using Kral_InvApp.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace Kral_InvApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        [HttpPost]
        public IActionResult Register(string email, string password)
        {
            Console.WriteLine("REGISTER POST CALLED");

            var user = new User
            {
                email = email,
                password_hash = HashPassword(password)
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var hash = HashPassword(password);

            var user = _context.Users
                .FirstOrDefault(u => u.email == email && u.password_hash == hash);

            if (user == null)
            {
                ViewBag.Error = "Špatný email nebo heslo";
                return View();
            }

            // uložíme uživatele do session
            HttpContext.Session.SetInt32("UserId", user.user_id);

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }





    }
}
