using JobSityChat.Core.DTOs;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace JobSityChat.Web.Business
{
    public class ChatMessageConsumer : IConsumer<ChatReceivedMessage>
    {
        private readonly IHubContext<ChatHubService> _context;

        public ChatMessageConsumer(IHubContext<ChatHubService> chatHubContext)
        {
            _context = chatHubContext;
        }
        public async Task Consume(ConsumeContext<ChatReceivedMessage> context)
        {
            var message = context.Message;                        
            await _context.Clients.All.SendAsync("ReceivedMessage", message);
        }
    }
}
