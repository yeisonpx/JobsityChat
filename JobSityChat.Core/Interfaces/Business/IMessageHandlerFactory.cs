using JobSityChat.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobSityChat.Core.Interfaces.Business
{
    public interface IMessageHandlerFactory
    {
        IMessageHandler Get(string message);
    }
}
