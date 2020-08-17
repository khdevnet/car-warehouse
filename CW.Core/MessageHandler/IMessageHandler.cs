using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CW.Core.MessageHandler
{
    public interface IMessageHandler<TMessage> where TMessage: IMessage
    {
        Task Handle(TMessage message);
    }
}
