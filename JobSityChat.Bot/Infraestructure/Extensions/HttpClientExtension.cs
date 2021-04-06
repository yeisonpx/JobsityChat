using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobSityChat.Bot.Infraestructure.Extensions
{
    public static class HttpClientExtension
    {
        public static IServiceCollection AddHttpClients(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddHttpClient("StockMarketClient", config =>
            {
                config.BaseAddress = new Uri(configuration["AppSettings:StockClientUri"]);
            });
            return services;
        }
    }
}
