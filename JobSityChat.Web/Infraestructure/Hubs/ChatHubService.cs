using JobSityChat.Core.Business;
using JobSityChat.Core.DTOs;
using JobSityChat.Core.Interfaces.Business;
using JobSityChat.Entities;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSityChat.Web.Business
{
    public class ChatHubService : Hub, IChatHubService
    {
        private readonly IMessageHandlerFactory _handlerFactory;

        public ChatHubService(IMessageHandlerFactory factory)
        {
            _handlerFactory = factory;
        }

        public async Task SendMessage(ChatMessageRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.Message)) { 
                request.UserName = Context.User.Identity.Name;
                var handler = _handlerFactory.Get(request.Message.Trim());
                await handler.ExecuteAsync(request);
            }
        }
    }
}
