using RabbitMQ.Client.Events;
using System;

namespace JobSityChat.Core.Interfaces.Business
{
    public interface IRabbitMQService
    {
        void ComsumQueue(string queue, EventHandler<BasicDeliverEventArgs> eventHandler);
        void SendMessage<T>(string queue, T message, string exchange = "");
    }
}