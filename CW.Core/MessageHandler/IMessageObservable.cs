using System;
using System.Collections.Generic;
using System.Text;

namespace CW.Core.MessageHandler
{
    public interface IMessageObservable<TMessage> where TMessage: IMessage
    {
        void Publish(TMessage eventMessage);
        void Subscribe(string subscriberName, Action<TMessage> action);
    }
}
