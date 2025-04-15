using Microsoft.AspNetCore.Mvc;
using StockAPIUsingHttpClient.Models;
using StockAPIUsingHttpClient.Services;

namespace StockAPIUsingHttpClient.Components
{
    public class CompanyViewComponent : ViewComponent
    {
        private readonly FinnhubService finnhubService;

        public CompanyViewComponent(FinnhubService finnhubService)
        {
            this.finnhubService = finnhubService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string stockSymbol = "MSFT")
        {
            var response = await finnhubService.GetCompanyProfileAsync(stockSymbol);

            if(response.Count <= 1 )
            {
                return View(new CompanyProfile() { Symbol = stockSymbol, Country = "Unknown", Exchange = "Unknown", Currency = "Unknown", Ipo = "01/01/2025", Name = "Not Found"});

            }
            var stockResponse = await finnhubService.GetStockQuoteAsync(stockSymbol);

            Stock stock = new();
            if (stockResponse != null)
            {

                stock = new Stock()
                {
                    StockSymbol = stockSymbol,
                    CurrentPrice = Convert.ToDouble(stockResponse["c"]?.ToString()),
                    HighestPrice = Convert.ToDouble(stockResponse["h"]?.ToString()),
                    LowestPrice = Convert.ToDouble(stockResponse["l"]?.ToString()),
                    OpenPrice = Convert.ToDouble(stockResponse["o"]?.ToString()),
                };
            }

            CompanyProfile companyProfile = new CompanyProfile()
            {
                Symbol = stockSymbol,
                Name = response["name"]?.ToString(),
                Country = response["country"]?.ToString(),
                Currency = response["currency"]?.ToString(),
                Exchange = response["exchange"]?.ToString(),
                Ipo = response["ipo"].ToString(),
                MarketCapitalization = (Convert.ToDecimal(stock?.CurrentPrice) * Convert.ToDecimal(response["shareOutstanding"]?.ToString()) * Convert.ToDecimal(1000000)),
                Logo = response["logo"].ToString(),
                FinnhubIndustry = response["finnhubIndustry"].ToString()
            };


            return View(companyProfile);
        }
    }
}
