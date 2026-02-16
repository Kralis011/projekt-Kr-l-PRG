using Kral_InvApp.Data;
using Kral_InvApp.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace Kral_InvApp.Controllers
{
    public class InvestmentController : Controller
    {
        private readonly AppDbContext _context;

        public InvestmentController(AppDbContext context)
        {
            _context = context;
        }

        // =============================
        // VÝPIS INVESTIC
        // =============================
        public IActionResult Index(int portfolioId)
        {
            var investments = _context.Investments
                .Where(i => i.PortfolioId == portfolioId)
                .ToList();

            decimal invested = 0;
            decimal current = 0;

            foreach (var i in investments)
            {
                invested += i.Amount * i.BuyPrice;

                if (i.SellPrice != null)
                    current += i.Amount * i.SellPrice.Value;
                else
                    current += i.Amount * i.BuyPrice;
            }

            decimal profit = current - invested;

            ViewBag.Invested = invested;
            ViewBag.Current = current;
            ViewBag.Profit = profit;
            ViewBag.PortfolioId = portfolioId;

            return View(investments);
        }

        // =============================
        // FORMULÁŘ
        // =============================
        public IActionResult Create(int portfolioId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var portfolioExists = _context.Portfolios
                .Any(p => p.PortfolioId == portfolioId && p.UserId == userId);

            if (!portfolioExists)
                return Content("Portfolio neexistuje");

            ViewBag.PortfolioId = portfolioId;
            return View();
        }

        // =============================
        // ULOŽENÍ INVESTICE
        // =============================
        [HttpPost]
        public IActionResult Create(
            int portfolioId,
            string assetName,
            string assetType,
            decimal amount,
            decimal buyPrice,
            decimal? sellPrice,
            DateTime tradeDate)
        {
            var investment = new Investment
            {
                AssetName = assetName,
                AssetType = assetType,
                Amount = amount,
                BuyPrice = buyPrice,
                SellPrice = sellPrice,
                TradeDate = tradeDate,
                PortfolioId = portfolioId
            };

            _context.Investments.Add(investment);
            _context.SaveChanges();

            return RedirectToAction("Index", new { portfolioId });
        }

        [HttpPost]

        public IActionResult Delete(int id, int portfolioId)

        {

            var investment = _context.Investments

                .FirstOrDefault(i => i.InvestmentId == id && i.PortfolioId == portfolioId);



            if (investment == null)

                return Content("Investice nenalezena");



            _context.Investments.Remove(investment);

            _context.SaveChanges();



            return RedirectToAction("Index", new { portfolioId });

        }
        
        public IActionResult Edit(int id, int portfolioId)
        {
            var investment = _context.Investments
                .FirstOrDefault(i => i.InvestmentId == id);

            if (investment == null)
                return NotFound();

            ViewBag.PortfolioId = portfolioId;

            return View(investment);
        }
        [HttpPost]
        public IActionResult Edit(Investment model, int portfolioId)
        {
            var investment = _context.Investments
                .FirstOrDefault(i => i.InvestmentId == model.InvestmentId);

            if (investment == null)
                return NotFound();

            investment.AssetName = model.AssetName;
            investment.AssetType = model.AssetType;
            investment.Amount = model.Amount;
            investment.BuyPrice = model.BuyPrice;
            investment.SellPrice = model.SellPrice;
            investment.TradeDate = model.TradeDate;

            _context.SaveChanges();

            return RedirectToAction("Index", new { portfolioId });
        }
        public IActionResult Chart(int portfolioId)
        {
            var investments = _context.Investments
                .Where(i => i.PortfolioId == portfolioId)
                .OrderBy(i => i.TradeDate)
                .ToList();

            var labels = investments
                .Select(i => i.TradeDate.ToString("dd.MM.yyyy"))
                .ToList();

            List<decimal> investedValues = new();
            List<decimal> currentValues = new();

            decimal investedSum = 0;
            decimal currentSum = 0;

            foreach (var i in investments)
            {
                investedSum += i.Amount * i.BuyPrice;

                currentSum += (i.SellPrice.HasValue ? i.SellPrice.Value : i.BuyPrice) * i.Amount;

                investedValues.Add(investedSum);
                currentValues.Add(currentSum);
            }

            ViewBag.Labels = labels;
            ViewBag.InvestedValues = investedValues;
            ViewBag.CurrentValues = currentValues;
            ViewBag.PortfolioId = portfolioId;

            return View();
        }




    }
}
