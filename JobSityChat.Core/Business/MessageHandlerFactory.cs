using JobSityChat.Common.Settings;
using JobSityChat.Core.Business.MessageHandlers;
using JobSityChat.Core.Interfaces.Business;
using MassTransit;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobSityChat.Core.Business
{
    public class MessageHandlerFactory : IMessageHandlerFactory
    {
        private IBus _bus;
        private IOptions<RabbitMQSettings> _settings;

        public MessageHandlerFactory(IBus bus,
            IOptions<RabbitMQSettings> settings)
        {
            _bus = bus;
            _settings = settings;
        }
        
        public IMessageHandler Get(string message)
        {
            if (message.StartsWith("/stock="))
            {
                return new StockMessageHandler(_bus, _settings);
            }
            return new DefaultMessageHandler(_bus, _settings);
        }
    }
}
