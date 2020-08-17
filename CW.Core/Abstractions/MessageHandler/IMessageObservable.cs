using CW.Core.Abstractions.MessageHandler.Dto;
using System;

namespace CW.Core.Abstractions.MessageHandler
{
    public interface IMessageObservable<TMessage> where TMessage: IMessage
    {
        void Publish(TMessage eventMessage);
        void Subscribe(string subscriberName, Action<TMessage> action);
    }
}
