using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

namespace JobSityChat.Core.Proxies
{
    public class StockMarketProxy : IStockMarketProxy
    {
        private IHttpClientFactory _httpClientFactory;
        private const string HTTP_FILE_DONWLOAD_REQUEST_EXCEPTION_MESSAGE = "Error downloading file with stockCode:{0}";
        public StockMarketProxy(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<Stream> GetStockAsync(string stockCode)
        {
            var client = _httpClientFactory.CreateClient("StockMarketClient");
            var response = await client.GetAsync($"?s={stockCode}&f=sd2t2ohlcv&h&e=csv");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStreamAsync();
            }

            throw new HttpRequestException(string.Format(HTTP_FILE_DONWLOAD_REQUEST_EXCEPTION_MESSAGE, stockCode));
        }
    }
}
