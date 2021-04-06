using JobSityChat.Bot.Infraestructure.Exceptions;
using JobSityChat.Common.Settings;
using JobSityChat.Core.DTOs;
using JobSityChat.Core.Proxies;
using MassTransit;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JobSityChat.Bot.Infraestructure.Consumers
{
    public class StockMessageConsumer : IConsumer<StockRequest>
    {
        private AppSettings _appSettings;
        private IBus _bus;
        private RabbitMQSettings _rabbitMqsettings;
        private IStockMarketProxy _stockMarketProxy;
        public StockMessageConsumer(IStockMarketProxy stockMarketProxy,
            IBus bus,
            IOptions<RabbitMQSettings> rabbitMqSettings,
            IOptions<AppSettings> appOptions
            )
        {
            _stockMarketProxy = stockMarketProxy;
            _bus = bus;
            _rabbitMqsettings = rabbitMqSettings.Value;
            _appSettings = appOptions.Value;
        }

        public async Task Consume(ConsumeContext<StockRequest> context)
        {
            try { 
                if (string.IsNullOrEmpty(context.Message.StockCode)) {                
                    throw new InvalidBotCommandException();
                }
                var file = await _stockMarketProxy.GetStockAsync(context.Message.StockCode.Trim());
                string message = ParseFileToMessage(file);
                ChatReceivedMessage messageRequest = BuildBotMessage(message);              
                await SendChatMessage(messageRequest);
            }
            catch(HttpRequestException ex)
            {
                await HandlerBotError(new InvalidBotCommandException(ex.Message));
            }
            catch(InvalidBotCommandException ex)
            {
                await HandlerBotError(ex);
            }
        }
        private ChatReceivedMessage BuildBotMessage(string message)
        {
            return new ChatReceivedMessage
            {
                Date = DateTime.Now,
                UserName = _appSettings.BotName,
                TargetUser = string.Empty,
                Message = message
            };
        }

        private async Task HandlerBotError(InvalidBotCommandException ex)
        {
            ChatReceivedMessage messageRequest = BuildBotMessage(ex.Message);
            await SendChatMessage(messageRequest);
        }
        private string ParseFileToMessage(Stream file)
        {
            StreamReader reader = new StreamReader(file);
            reader.ReadLine();
            string[] content = reader.ReadLine().Split(',');
            return $"{content[0]} quote is ${content[3]} per share";
        }

        private async Task SendChatMessage(ChatReceivedMessage messageRequest)
        {
            Uri uri = new Uri($"{_rabbitMqsettings.Uri}{_rabbitMqsettings.ChatEndpoint}");
            var endPoint = await _bus.GetSendEndpoint(uri);
            await endPoint.Send(messageRequest);
        }
    }
}
