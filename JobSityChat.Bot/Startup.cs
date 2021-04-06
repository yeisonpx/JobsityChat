using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobSityChat.Bot.Infraestructure;
using JobSityChat.Bot.Infraestructure.Consumers;
using JobSityChat.Bot.Infraestructure.Extensions;
using JobSityChat.Bot.Infraestructure.Middleware;
using JobSityChat.Common.Settings;
using JobSityChat.Core.Business;
using JobSityChat.Core.Interfaces.Business;
using JobSityChat.Core.Proxies;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace JobSityChat.Bot
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IStockMarketProxy, StockMarketProxy>();
            services.Configure<RabbitMQSettings>(Configuration.GetSection("RabbitMQSettings"));
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddHttpClients(Configuration);
            services.AddMassTransitConfig(Configuration);
            services.AddControllers();            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<BotExceptionMiddleware>();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();               
            });

        }
    }
}
