using System;
using System.Collections.Generic;
using System.Text;

namespace JobSityChat.Common.Settings
{
    public class RabbitMQSettings
    {
        public string Uri { get; set; }
        public string ChatEndpoint { get; set; }
        public string StockEndpoint { get; set; }        
    }
}
