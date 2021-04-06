using JobSityChat.Core.Interfaces.Business;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSityChat.Core.Business
{
    public class RabbitMQService : IRabbitMQService
    {
        public void SendMessage<T>(string queue, T message, string exchange = "")
        {
            using var channel = GetChannel();
            channel.QueueDeclare(queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            var payload = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            channel.BasicPublish(exchange, queue, null, payload);
        }
        public void ComsumQueue(string queue, EventHandler<BasicDeliverEventArgs> eventHandler)
        {
            using var channel = GetChannel();
            channel.QueueDeclare(queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += eventHandler;
            channel.BasicConsume(queue, true, consumer);
        }

        private IModel GetChannel()
        {
            IConnection connection = GetConnection();
            return connection.CreateModel();
        }
        private static IConnection GetConnection()
        {
            var factory = new ConnectionFactory()
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            var connection = factory.CreateConnection();
            return connection;
        }

    }
}
