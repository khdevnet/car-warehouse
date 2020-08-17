using System;
using System.Collections.Generic;
using System.Text;

namespace CW.Core.MessageHandler
{
    public abstract class MessageBase : IMessage
    {
        public MessageBase(Guid messageId, DateTime timestamp)
        {
            MessageId = messageId;
            Timestamp = timestamp;
        }
        public Guid MessageId { get; }
        public DateTime Timestamp { get; }

    }
}
