using JobSityChat.Bot.Infraestructure.Exceptions;
using JobSityChat.Common.Settings;
using JobSityChat.Core.DTOs;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobSityChat.Bot.Infraestructure.Middleware
{
    public class BotExceptionMiddleware
    {
        private RequestDelegate _Next;

        public BotExceptionMiddleware(RequestDelegate next)
        {
            _Next = next;
        }
        public async Task Invoke(HttpContext context, 
            IBus bus, 
            IOptions<RabbitMQSettings> options)
        {
            try
            {
                await   _Next(context);   
            }catch(Exception ex)
            {
                switch (ex)
                {
                    case InvalidBotCommandException a:
                        var settings = options.Value;
                        var messageRequest = new ChatReceivedMessage
                        {
                            Date = DateTime.Now,
                            UserName = "Bot",
                            Message = a.Message,
                            TargetUser = string.Empty,
                        };
                        Uri uri = new Uri($"{settings.Uri}{settings.ChatEndpoint}");
                        var endPoint = await bus.GetSendEndpoint(uri);
                        await endPoint.Send(messageRequest);
                        break;
                    default:
                        throw ex;
                }
            }
        }
    }
}
