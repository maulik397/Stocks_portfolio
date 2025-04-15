using Microsoft.AspNetCore.Mvc;
using StockAPIUsingHttpClient.Models;
using StockAPIUsingHttpClient.Services;

namespace StockAPIUsingHttpClient.Components
{
    public class StockViewComponent : ViewComponent
    {
        private readonly FinnhubService finnhub;

        public StockViewComponent(FinnhubService finnhub)
        {
            this.finnhub = finnhub;
        }
        //[HttpGet]
        //[Route("[]")]
        public async Task<IViewComponentResult> InvokeAsync(string stockSymbol)
        {
            var response = await finnhub.GetStockQuoteAsync(stockSymbol);
            if(response == null || response.Count <=1)
            {
                return View();
            }
            Stock stock = new Stock()
            {
                StockSymbol = stockSymbol,
                CurrentPrice = Convert.ToDouble(response["c"]?.ToString()),
                HighestPrice = Convert.ToDouble(response["h"]?.ToString()),
                LowestPrice = Convert.ToDouble(response["l"]?.ToString()),
                OpenPrice = Convert.ToDouble(response["o"]?.ToString()),
            };
            return View(stock);
        }
    }
}
