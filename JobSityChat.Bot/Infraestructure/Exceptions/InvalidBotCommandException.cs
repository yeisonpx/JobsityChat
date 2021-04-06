using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobSityChat.Bot.Infraestructure.Exceptions
{
    public class InvalidBotCommandException : Exception
    {
        private const string INVALID_BOT_EXCEPTION_MESSAGE = "Invalid command for stock bot.";
        public InvalidBotCommandException(): base(INVALID_BOT_EXCEPTION_MESSAGE) { }
        public InvalidBotCommandException( string message) : base(message)
        {
        }
    }
}
