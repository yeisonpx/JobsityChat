using JobSityChat.Common.Settings;
using JobSityChat.Core.DTOs;
using JobSityChat.Core.Interfaces.Business;
using MassTransit;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobSityChat.Core.Business.MessageHandlers
{
    public class StockMessageHandler :  IMessageHandler
    {
        private IBus _bus;
        private RabbitMQSettings _settings;

        public StockMessageHandler(IBus bus,
            IOptions<RabbitMQSettings> settings
            )
        {
            _bus = bus;
            _settings = settings.Value;
        }

        public async Task ExecuteAsync(ChatMessageRequest request)
        {
            string stockCode = request.Message.Replace("/stock=","");
            Uri uri = new Uri($"{_settings.Uri}{_settings.StockEndpoint}");
            var endPoint = await _bus.GetSendEndpoint(uri);            
            await endPoint.Send(new StockRequest { 
                StockCode = stockCode 
            });
        }
    }
}
