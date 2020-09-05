using Microsoft.AspNetCore.Mvc;
using ServerApp.Models;
using System.Diagnostics;

namespace ServerApp.Controllers
{
    public class HomeController : Controller
    {
        readonly DataContext context;

        public HomeController(DataContext ctx) => context = ctx;

        public IActionResult Index() => View(context.Contacts);

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
