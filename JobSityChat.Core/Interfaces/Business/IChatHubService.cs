using JobSityChat.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobSityChat.Core.Business
{
    public interface IChatHubService
    {
        public Task SendMessage(ChatMessageRequest request);        
    }
}
