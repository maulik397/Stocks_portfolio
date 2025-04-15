using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using StockAPIUsingHttpClient.Services;

namespace StockAPIUsingHttpClient.Controllers
{
    public class StockController : Controller
    {
        private readonly FinnhubService finnhubService;
        private readonly ILogger<StockController> logger;

        public StockController(FinnhubService finnhubService, ILogger<StockController> logger)
        {
            this.finnhubService = finnhubService;
            this.logger = logger;
        }

        [HttpGet]
        [Route("[Controller]/[Action]/{symbol?}")]
        public async Task<IActionResult> GetStockInfo([FromRoute] string symbol)
        {
            logger.LogInformation("Fetching Stock Info!!");
            logger.LogWarning("Fetching Stock Info!!");
            return ViewComponent("Stock", new { stockSymbol = symbol });
        }

        [HttpGet]
        [Route("[Controller]/[Action]/{symbol?}")]
        public async Task<IActionResult> GetCompanyProfile([FromRoute] string symbol)
        {
            logger.LogInformation("Fetching Company Info!!");

            return ViewComponent("Company", new { stockSymbol = symbol });
        }

        [HttpGet]
        [Route("[Controller]/[Action]/{symbol?}")]
        public async Task<IActionResult> GetCompanyFinance([FromRoute] string symbol)
        {
            logger.LogInformation("Fetching Company Financials Info!!");

            return ViewComponent("CompanyFinancials", new { stockSymbol = symbol });
        }
        [HttpGet]
        [Route("[Controller]/[Action]/{symbol?}")]
        public async Task<IActionResult> AddToPortfolio([FromRoute] string symbol)
        {
            logger.LogInformation("Adding Company To Portfolio!!");


            var companyProfileResponse = await finnhubService.GetCompanyProfileAsync(symbol);

            if(companyProfileResponse.Count <= 1)
            {
                TempData["Message"] = "Invalid Symbol!!";
                return RedirectToAction("Index", "Portfolio");
            }

            if (Request.Cookies["portfolio"] == null)
            {
                List<string> list = new List<string> { symbol };

                Response.Cookies.Append("portfolio", JsonSerializer.Serialize(list), new CookieOptions {
                    Expires = DateTimeOffset.UtcNow.AddYears(1), 
                    HttpOnly = true, 
                    Secure = true,
                    SameSite = SameSiteMode.Lax
                });

                return RedirectToAction("Index", "Portfolio");
            }
            var symbolList = JsonSerializer.Deserialize<List<string>>(Request.Cookies["portfolio"]);
            if (!symbolList.Contains(symbol))
            {
                symbolList.Add(symbol);
            }
            else
            {
                TempData["Message"] = "Stock Already Exist In The Portfolio!!";
            }
            Response.Cookies.Append("portfolio", JsonSerializer.Serialize(symbolList), new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddYears(1),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax
            });
            return RedirectToAction("Index", "Portfolio");
        }

        [HttpGet]
        [Route("[Controller]/[Action]/{symbol?}")]
        public async Task<IActionResult> RemoveFromPortfolio([FromRoute] string symbol)
        {
            logger.LogInformation("Removing Company From Portfolio!!");

            if (Request.Cookies["portfolio"] != null)
            {
                List<string> list = new List<string> { symbol };
                var symbolList = JsonSerializer.Deserialize<List<string>>(Request.Cookies["portfolio"]);
                if (symbolList.Contains(symbol))
                {
                    symbolList.Remove(symbol);
                }
                Response.Cookies.Append("portfolio", JsonSerializer.Serialize(symbolList), new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1),
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Lax
                });
                return RedirectToAction("Index", "Portfolio");
            }
            
            return RedirectToAction("Index", "Portfolio");
        }
    }
}
