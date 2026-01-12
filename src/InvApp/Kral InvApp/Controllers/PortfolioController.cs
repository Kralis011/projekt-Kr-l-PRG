using Microsoft.AspNetCore.Mvc;

namespace Kral_InvApp.Controllers
{
    public class PortfolioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


    }
}
