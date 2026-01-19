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
                return RedirectToAction("Login", "Account");

            var portfolios = _context.Portfolios
                .Where(p => p.UserId == userId)
                .ToList();

            var investments = _context.Investments
                .Where(i => portfolios.Select(p => p.PortfolioId).Contains(i.PortfolioId))
                .ToList();

            decimal invested = investments.Sum(i => i.Amount * i.BuyPrice);

            decimal currentValue = investments.Sum(i =>
                i.SellPrice != null
                    ? i.Amount * i.SellPrice.Value
                    : i.Amount * i.BuyPrice
            );

            ViewBag.PortfolioCount = portfolios.Count;
            ViewBag.InvestmentCount = investments.Count;
            ViewBag.Invested = invested;
            ViewBag.CurrentValue = currentValue;
            ViewBag.Profit = currentValue - invested;

            return View();
        }

    }


}
