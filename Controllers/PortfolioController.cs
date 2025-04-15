using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using StockAPIUsingHttpClient.Models;
using StockAPIUsingHttpClient.Services;

namespace StockAPIUsingHttpClient.Controllers
{
    [Route("[Controller]/[Action]")]
    public class PortfolioController :Controller
    {
        private readonly FinnhubService finnhubService;

        public PortfolioController(FinnhubService finnhubService)
        {
            this.finnhubService = finnhubService;
        }
        /*
         //%5B%22BLK%22%2C%22ORCL%22%2C%22BCSF%22%5D   
         %5B  = [ %22  = " %2C  = , %5D  = ]
        ["BLK", "ORCL", "BCSF"]
        break down 
         store companyprofiles in company profiles model --
            1) check cookies 
            2) desaerilize them add them in for loop 
            3) get company profile from symbols 
            4) get stocks info 
         */
        [HttpGet]
        [Route("~/Portfolio")]
        public async Task<IActionResult> Index()
        {
            
            List<CompanyProfile> companyProfiles = new();
            if (Request.Cookies["portfolio"] != null)
            {   
                var symbolList = JsonSerializer.Deserialize<List<string>>(Request.Cookies["portfolio"]);
                foreach (var stockSymbol in symbolList)
                {
                    var response = await finnhubService.GetCompanyProfileAsync(stockSymbol);

                    if (response.Count == 0)
                    {
                        return View(new CompanyProfile() { Symbol = stockSymbol, Country = "Unknown", Exchange = "Unknown", Currency = "Unknown", Ipo = "01/01/2025", Name = "Not Found" });

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
                    companyProfiles.Add(companyProfile);
                }

            }
            return View(companyProfiles);
        }

        [HttpGet]
        [Route("{symbol?}")]
        public async Task<IActionResult> Details([FromRoute] string symbol)
        {
            var companyProfileResponse = await finnhubService.GetCompanyProfileAsync(symbol);
            var financialsResponse = await finnhubService.GetCompanyFinancialsAsync(symbol);
            var stockResponse = await finnhubService.GetStockQuoteAsync(symbol); 

            Stock stock = new();
            if (stockResponse != null || stockResponse.Count <= 1)
            {

                stock = new Stock()
                {
                    StockSymbol = symbol,
                    CurrentPrice = Convert.ToDouble(stockResponse["c"]?.ToString()),
                    HighestPrice = Convert.ToDouble(stockResponse["h"]?.ToString()),
                    LowestPrice = Convert.ToDouble(stockResponse["l"]?.ToString()),
                    OpenPrice = Convert.ToDouble(stockResponse["o"]?.ToString()),
                };
            }


            CompanyProfile companyProfile = new();
            if (companyProfileResponse.Count <= 1)
            {
                companyProfile = new CompanyProfile() { Symbol = symbol, Country = "Unknown", Exchange = "Unknown", Currency = "Unknown", Ipo = "01/01/2025", Name = "Not Found" };

            }
            else
            {
                companyProfile = new CompanyProfile()
                {
                    Symbol = symbol,
                    Name = companyProfileResponse["name"]?.ToString(),
                    Country = companyProfileResponse["country"]?.ToString(),
                    Currency = companyProfileResponse["currency"]?.ToString(),
                    Exchange = companyProfileResponse["exchange"]?.ToString(),
                    Ipo = companyProfileResponse["ipo"].ToString(),
                    MarketCapitalization = (Convert.ToDecimal(stock?.CurrentPrice) * Convert.ToDecimal(companyProfileResponse["shareOutstanding"]?.ToString()) * Convert.ToDecimal(1000000)),
                    Logo = companyProfileResponse["logo"].ToString(),
                    FinnhubIndustry = companyProfileResponse["finnhubIndustry"].ToString()
                };
            }

            
            
            var metricData = financialsResponse["metric"]?.ToString();
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
            
           
            var viewModel = new CompanyDetailsViewModel
            {
                CompanyProfile = companyProfile,
                BasicCompanyFinancials = basicCompanyFinance,
                Stock = stock
            };

            return View(viewModel);
        }


    }
}
