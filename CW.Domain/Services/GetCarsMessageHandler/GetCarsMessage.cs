using CW.Core.MessageHandler;
using System;
using System.Collections.Generic;
using System.Text;

namespace CW.Domain.Services.GetCarsMessageHandler
{
    public class GetCarsMessage : MessageBase
    {
        public GetCarsMessage(Guid messageId, DateTime timestamp): base(messageId, timestamp) { }
    }
}
