using CW.Core.MessageHandler;
using System;
using System.Collections.Concurrent;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace CW.Core.MessageHandler
{
    internal class MessageObservable<TMessage> : IMessageObservable<TMessage> where TMessage : IMessage
    {
        private readonly Subject<TMessage> _subject;
        private readonly ConcurrentDictionary<string, IDisposable> _subscribers;

        public MessageObservable()
        {
            _subject = new Subject<TMessage>();
            _subscribers = new ConcurrentDictionary<string, IDisposable>();
        }

        public void Publish(TMessage eventMessage)
        {
            _subject.OnNext(eventMessage);
        }

        public void Subscribe(string subscriberName, Action<TMessage> action)
        {
            if (!_subscribers.ContainsKey(subscriberName))
            {
                _subscribers.TryAdd(subscriberName, _subject.Subscribe(action));
            }
        }

        public void Subscribe(string subscriberName, Func<TMessage, bool> predicate, Action<TMessage> action)
        {
            if (!_subscribers.ContainsKey(subscriberName))
            {
                _subscribers.TryAdd(subscriberName, _subject.Where(predicate).Subscribe(action));
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
