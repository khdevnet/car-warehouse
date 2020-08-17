using System;
using System.Collections.Generic;
using System.Text;

namespace CW.Core.Abstractions.MessageHandler.Dto
{
    public interface IMessage
    {
        Guid MessageId { get; }
        DateTime Timestamp { get; }
    }
}
