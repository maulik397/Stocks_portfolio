using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using StockAPIUsingHttpClient.Models;
using StockAPIUsingHttpClient.Services;

namespace StockAPIUsingHttpClient.Components
{
    public class CompanyFinancialsViewComponent : ViewComponent
    {
        private readonly FinnhubService finnhubService;

        public CompanyFinancialsViewComponent(FinnhubService finnhubService)
        {
            this.finnhubService = finnhubService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string stockSymbol = "MSFT")
        {
            var responseFinance = await finnhubService.GetCompanyFinancialsAsync(stockSymbol);
            var responseProfile = await finnhubService.GetCompanyProfileAsync(stockSymbol);

            if (responseFinance.Count <= 1)
            {
                return View(new BasicCompanyFinancials() {});

            }
            CompanyProfile companyProfile = null;
            if (responseProfile.Count <= 1)
            {
                companyProfile = new CompanyProfile() { Symbol = stockSymbol, Country = "Unknown", Exchange = "Unknown", Currency = "Unknown", Ipo = "01/01/2025", Name = "Not Found" };

            }
            else
            {
                companyProfile = new CompanyProfile()
                {
                    Symbol = stockSymbol,
                    Name = responseProfile["name"]?.ToString(),
                    Country = responseProfile["country"]?.ToString(),
                    Currency = responseProfile["currency"]?.ToString(),
                    Exchange = responseProfile["exchange"]?.ToString(),
                    Ipo = responseProfile["ipo"].ToString(),
                    MarketCapitalization = (Convert.ToDecimal(responseProfile["marketCapitalization"].ToString()) * Convert.ToDecimal(responseProfile["shareOutstanding"].ToString())),
                    Logo = responseProfile["logo"].ToString(),
                    FinnhubIndustry = responseProfile["finnhubIndustry"].ToString()
                };
            }

            

            var metricData = responseFinance["metric"]?.ToString();
            BasicCompanyFinancials basicCompanyFinance = new();

            if (!string.IsNullOrEmpty(metricData))
            {
                basicCompanyFinance = JsonSerializer.Deserialize<BasicCompanyFinancials>(metricData);
            }

            basicCompanyFinance.Name = companyProfile.Name;
            basicCompanyFinance.Logo = companyProfile.Logo;
            basicCompanyFinance.Country = companyProfile.Country;
            basicCompanyFinance.Currency = companyProfile.Currency;
            basicCompanyFinance.FinnhubIndustry = companyProfile.FinnhubIndustry;
            basicCompanyFinance.MarketCapitalization = companyProfile.MarketCapitalization;
            return View(basicCompanyFinance);
        }
    }
}
