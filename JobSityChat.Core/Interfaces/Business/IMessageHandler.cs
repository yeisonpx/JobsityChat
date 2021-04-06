using JobSityChat.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobSityChat.Core.Interfaces.Business
{
    public interface IMessageHandler
    {
        Task ExecuteAsync(ChatMessageRequest request);
    }
}
