using System;

namespace SoyalEventServer
{
    public class SoyalEventLogException : Exception
    {
        public string EvetMessage { get; set; }
        public SoyalEventLogException(string message)
        {
            EvetMessage = message;
        }
    }
}
