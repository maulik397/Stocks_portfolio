﻿namespace StockAPIUsingHttpClient.Models
{
    public class Stock
    {
        public string StockSymbol { get; set; } = "MSFT";
        public double? CurrentPrice { get; set; }
        public double? HighestPrice { get; set; }
        public double? LowestPrice { get; set; }
        public double? OpenPrice { get; set; }
        //double ClosePrice { get; set; }

    }
}
