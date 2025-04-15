namespace StockAPIUsingHttpClient.Models
{
    public class CompanyProfile
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }
        public string Exchange { get; set; }
        public string Ipo { get; set; }
        public decimal MarketCapitalization { get; set; }
        public string FinnhubIndustry { get; set; }
        public string Logo { get; set; }
    }
}
