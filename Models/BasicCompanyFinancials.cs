using System.Text.Json.Serialization;

namespace StockAPIUsingHttpClient.Models
{
    public class BasicCompanyFinancials : CompanyProfile
    {
        [JsonPropertyName("10DayAverageTradingVolume")]
        public decimal? TenDayAverageTradingVolume { get; set; }

        [JsonPropertyName("13WeekPriceReturnDaily")]
        public decimal? ThirteenWeekPriceReturnDaily { get; set; }

        [JsonPropertyName("26WeekPriceReturnDaily")]
        public decimal? TwentySixWeekPriceReturnDaily { get; set; }

        [JsonPropertyName("52WeekHigh")]
        public decimal? WeekHigh52 { get; set; }

        [JsonPropertyName("52WeekHighDate")]
        public string WeekHighDate52 { get; set; }

        [JsonPropertyName("52WeekLow")]
        public decimal? WeekLow52 { get; set; }

        [JsonPropertyName("52WeekLowDate")]
        public string WeekLowDate52 { get; set; }

        [JsonPropertyName("beta")]
        public decimal? Beta { get; set; }

        [JsonPropertyName("marketCapitalization")]
        public decimal? MarketCap { get; set; }

        [JsonPropertyName("5DayPriceReturnDaily")]
        public decimal? PriceReturn5Day { get; set; }

        [JsonPropertyName("priceRelativeToS\u0026P50052Week")]
        public decimal? PriceRelativeToSAndP50052Week { get; set; }

        [JsonPropertyName("priceRelativeToS\u0026P50026Week")]
        public decimal? PriceRelativeToSAndP50026Week { get; set; }

        [JsonPropertyName("priceRelativeToS\u0026P50013Week")]
        public decimal? PriceRelativeToSAndP50013Week { get; set; }

        [JsonPropertyName("priceRelativeToS\u0026P500Ytd")]
        public decimal? PriceRelativeToSAndP500Ytd { get; set; }

        [JsonPropertyName("revenueGrowth3Y")]
        public decimal? RevenueGrowth3Y { get; set; }

        [JsonPropertyName("revenueGrowth5Y")]
        public decimal? RevenueGrowth5Y { get; set; }

        [JsonPropertyName("epsGrowth3Y")]
        public decimal? EpsGrowth3Y { get; set; }

        [JsonPropertyName("epsGrowth5Y")]
        public decimal? EpsGrowth5Y { get; set; }

        [JsonPropertyName("epsGrowthQuarterlyYoy")]
        public decimal? EpsGrowthQuarterlyYoy { get; set; }

        [JsonPropertyName("epsGrowthTTMYoy")]
        public decimal? EpsGrowthTTMYoy { get; set; }

        [JsonPropertyName("epsAnnual")]
        public decimal? EpsAnnual { get; set; }

        [JsonPropertyName("epsTTM")]
        public decimal? EpsTTM { get; set; }

        [JsonPropertyName("revenuePerShareAnnual")]
        public decimal? RevenuePerShareAnnual { get; set; }

        [JsonPropertyName("revenuePerShareTTM")]
        public decimal? RevenuePerShareTTM { get; set; }

        [JsonPropertyName("grossMarginAnnual")]
        public decimal? GrossMarginAnnual { get; set; }

        [JsonPropertyName("netProfitMarginAnnual")]
        public decimal? NetProfitMarginAnnual { get; set; }

        [JsonPropertyName("operatingMarginAnnual")]
        public decimal? OperatingMarginAnnual { get; set; }

        [JsonPropertyName("pretaxMarginAnnual")]
        public decimal? PretaxMarginAnnual { get; set; }

        [JsonPropertyName("peAnnual")]
        public decimal? PeAnnual { get; set; }

        [JsonPropertyName("peTTM")]
        public decimal? PeTTM { get; set; }

        [JsonPropertyName("payoutRatioAnnual")]
        public decimal? PayoutRatioAnnual { get; set; }

        [JsonPropertyName("currentRatioAnnual")]
        public decimal? CurrentRatioAnnual { get; set; }

        [JsonPropertyName("currentRatioQuarterly")]
        public decimal? CurrentRatioQuarterly { get; set; }

        [JsonPropertyName("quickRatioAnnual")]
        public decimal? QuickRatioAnnual { get; set; }

        [JsonPropertyName("quickRatioQuarterly")]
        public decimal? QuickRatioQuarterly { get; set; }

        [JsonPropertyName("dividendPerShareAnnual")]
        public decimal? DividendPerShareAnnual { get; set; }

        [JsonPropertyName("dividendYieldIndicatedAnnual")]
        public decimal? DividendYieldIndicatedAnnual { get; set; }
    }
}
