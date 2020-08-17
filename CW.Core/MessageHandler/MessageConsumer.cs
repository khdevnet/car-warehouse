using CW.Core.Abstractions.MessageHandler;
using CW.Core.Abstractions.MessageHandler.Dto;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CW.Core.MessageHandler
{
    internal class MessageConsumer<TMessage> : BackgroundService, IDisposable where TMessage : IMessage
    {
        private readonly ILogger<MessageConsumer<TMessage>> _logger;
        private readonly IMessageHandler<TMessage> _messageHandler;
        private readonly IMessageObservable<TMessage> _messageObservable;
        private readonly CancelationTokenSourceProvider _cancelationTokenStore;

        public MessageConsumer(
          IMessageHandler<TMessage> messageHandler,
          IMessageObservable<TMessage> messageObservable,
          ILogger<MessageConsumer<TMessage>> logger,
          CancelationTokenSourceProvider cancelationTokenStore
          )
        {
            _messageHandler = messageHandler;
            _messageObservable = messageObservable;
            _cancelationTokenStore = cancelationTokenStore;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _messageObservable.Subscribe(subscriberName: typeof(MessageConsumer<>).Name,
                                    action: async (e) =>
                                    {
                                        if (e is IMessage)
                                        {
                                            _logger.LogInformation("");
                                            await ProcessMessage(e);
                                        }
                                    });

            return Task.CompletedTask;
        }

        public async Task ProcessMessage(TMessage message)
        {
            using (var tokenScope = _cancelationTokenStore.CreateScope(message.MessageId))
            {
              await  _messageHandler.Handle(message);
            }
        }
    }
}
