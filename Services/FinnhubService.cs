using System.Text.Json;

namespace StockAPIUsingHttpClient.Services
{
    public class FinnhubService
    {
        // using  Ihttpclient factory interface 
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;

        public FinnhubService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
        }

        //Task<T> represents an asynchronous operation that returns a result in the future. it can have no return type by declaring it as Task <> 
        //


        //it is convention what when u call http method which is async then method name should end with async  
        //this gives stok info 
        public async Task<Dictionary<string, Object?>> GetStockQuoteAsync(string StockSymbol = "MSFT")
        {
            //here using httpclent only because httpclientfactory only manages instaces of httpclient 

            using(HttpClient client = httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={StockSymbol}&token={configuration["FinnhubToken"]}"),
                    Method = HttpMethod.Get,
                };

                HttpResponseMessage responseMessage = await client.SendAsync(httpRequestMessage);    
                var content = await responseMessage.Content.ReadAsStringAsync();
                Dictionary<string, Object?> dictResponse = JsonSerializer.Deserialize<Dictionary<string,Object?>>(content);
                return dictResponse;
            }
        }
        //this gives company info 
        public async Task<Dictionary<string, Object?>> GetCompanyProfileAsync(string StockSymbol = "MSFT")
        {
            using (HttpClient client = httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/stock/profile2?symbol={StockSymbol}&token={configuration["FinnhubToken"]}"),
                    Method = HttpMethod.Get,
                };

                HttpResponseMessage responseMessage = await client.SendAsync(httpRequestMessage);
                var content = await responseMessage.Content.ReadAsStringAsync();
                Dictionary<string, Object?> dictResponse = JsonSerializer.Deserialize<Dictionary<string, Object?>>(content);
                return dictResponse;
            }
        } 
        public async Task<Dictionary<string, Object?>> GetCompanyFinancialsAsync(string StockSymbol = "MSFT")
        {
            using (HttpClient client = httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/stock/metric?symbol={StockSymbol}&token={configuration["FinnhubToken"]}"),
                    Method = HttpMethod.Get,
                };

                HttpResponseMessage responseMessage = await client.SendAsync(httpRequestMessage);
                var content = await responseMessage.Content.ReadAsStringAsync();
                Dictionary<string, Object?> dictResponse = JsonSerializer.Deserialize<Dictionary<string, Object?>>(content);
                return dictResponse;
            }
        }
    }
}
