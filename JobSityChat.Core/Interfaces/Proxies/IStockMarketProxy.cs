using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace JobSityChat.Core.Proxies
{
    public interface IStockMarketProxy
    {
        Task<Stream> GetStockAsync(string stockCode); 
    }
}
