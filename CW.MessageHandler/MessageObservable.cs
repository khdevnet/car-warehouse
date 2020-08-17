using CW.Core.MessageHandler;
using CW.Domain.Services.MessageHandler;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace CW.MessageHandler
{
    public class MessageObservable : IMessageObservable
    {
        private readonly Subject<IMessage> _subject;
        private readonly Dictionary<string, IDisposable> _subscribers;

        public MessageObservable()
        {
            _subject = new Subject<IMessage>();
            _subscribers = new Dictionary<string, IDisposable>();
        }

        public void Publish(IMessage eventMessage)
        {
            _subject.OnNext(eventMessage);
        }

        public void Subscribe(string subscriberName, Action<IMessage> action)
        {
            if (!_subscribers.ContainsKey(subscriberName))
            {
                _subscribers.Add(subscriberName, _subject.Subscribe(action));
            }
        }

        public void Subscribe(string subscriberName, Func<IMessage, bool> predicate, Action<IMessage> action)
        {
            if (!_subscribers.ContainsKey(subscriberName))
            {
                _subscribers.Add(subscriberName, _subject.Where(predicate).Subscribe(action));
            }
        }

        public void Dispose()
        {
            if (_subject != null)
            {
                _subject.Dispose();
            }

            foreach (var subscriber in _subscribers)
            {
                subscriber.Value.Dispose();
            }
        }
    }
}
