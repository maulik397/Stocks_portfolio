using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using StockAPIUsingHttpClient.Models;
using StockAPIUsingHttpClient.Services;

namespace StockAPIUsingHttpClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FinnhubService finnhubService;

        public HomeController(ILogger<HomeController> logger, FinnhubService finnhubService)
        {
            _logger = logger;
            this.finnhubService = finnhubService;
        }

        public  IActionResult Index() {
            return View();
        }
      
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
