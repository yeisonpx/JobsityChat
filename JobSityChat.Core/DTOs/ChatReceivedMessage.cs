using System;
using System.Collections.Generic;
using System.Text;

namespace JobSityChat.Core.DTOs
{
    public class ChatReceivedMessage
    {
        public DateTime Date { get; set; }
        public string UserName { get; set; }
        public string TargetUser { get; set; }
        public string Message { get; set; }
    }
}
