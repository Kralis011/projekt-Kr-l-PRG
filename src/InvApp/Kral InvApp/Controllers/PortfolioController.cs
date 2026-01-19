using Microsoft.AspNetCore.Mvc;

using Kral_InvApp.Data;
using Kral_InvApp.Entities;
using Microsoft.AspNetCore.Mvc;

public class PortfolioController : Controller
{
    private readonly AppDbContext _context;

    public PortfolioController(AppDbContext context)
    {
        _context = context;
    }

    // VÝPIS PORTFOLIÍ
    public IActionResult Index()
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
            return RedirectToAction("Login", "Account");

        var portfolios = _context.Portfolios
            .Where(p => p.UserId == userId)
            .ToList();

        return View(portfolios);
    }

    // FORMULÁŘ
    public IActionResult Create()
    {
        return View();
    }

    // ULOŽENÍ
    [HttpPost]
    public IActionResult Create(string name)
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
            return RedirectToAction("Login", "Account");

        var portfolio = new Portfolio
        {
            Name = name,
            UserId = userId.Value
        };

        _context.Portfolios.Add(portfolio);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
}
