using JobSityChat.Web.Business;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobSityChat.Web.Infraestructure.Extensions
{
    public static class MassTransitExtension
    {
        public static IServiceCollection AddMassTransitConfig(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<ChatMessageConsumer>();
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    config.UseHealthCheck(provider);
                    config.Host(new Uri(configuration["RabbitMQSettings:Uri"]), h =>
                    {
                        h.Username(configuration["RabbitMQSettings:User"]);
                        h.Password(configuration["RabbitMQSettings:User"]);
                    });

                    config.ReceiveEndpoint(configuration["RabbitMQSettings:ChatEndpoint"], ep =>
                    {
                        ep.PrefetchCount = 16;
                        ep.ConfigureConsumer<ChatMessageConsumer>(provider);
                    });
                }));
            });
            services.AddMassTransitHostedService();
            return services;
        }
    }
}
