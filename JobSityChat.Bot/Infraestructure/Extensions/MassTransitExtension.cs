
using JobSityChat.Bot.Infraestructure.Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobSityChat.Bot.Infraestructure.Extensions
{
    public static class MassTransitExtension
    {
        public static IServiceCollection AddMassTransitConfig(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    x.AddConsumer<StockMessageConsumer>();
                    config.UseHealthCheck(provider);
                    config.Host(new Uri(configuration["RabbitMQSettings:Uri"]), h =>
                    {
                        h.Username(configuration["RabbitMQSettings:User"]);
                        h.Password(configuration["RabbitMQSettings:Password"]);
                    });

                    config.ReceiveEndpoint(configuration["RabbitMQSettings:StockEndpoint"], ep =>
                    {
                        ep.PrefetchCount = 16;
                        ep.ConfigureConsumer<StockMessageConsumer>(provider);
                    });
                }));
            });
            services.AddScoped<StockMessageConsumer>();
            services.AddMassTransitHostedService();
            return services;
        }
    }
}
