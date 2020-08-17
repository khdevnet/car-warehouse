using CW.Core;
using CW.Core.MessageHandler;
using CW.Domain.Services.MessageHandler;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CW.MessageHandler
{
    public class MessageConsumer<TMessageHandler> : BackgroundService, IDisposable where TMessageHandler: IMessageHandler
    {
        private readonly ILogger<MessageObservable> _logger;
        private readonly TMessageHandler _messageHandler;
        private readonly IMessageObservable _messageSubject;
        private readonly CancelationTokenStore _cancelationTokenStore;

        public MessageConsumer(
            TMessageHandler messageHandler,
          IMessageObservable messageSubject,
          ILogger<MessageObservable> logger,
          CancelationTokenStore cancelationTokenStore
          )
        {
            _messageHandler = messageHandler;
            _messageSubject = messageSubject;
            _cancelationTokenStore = cancelationTokenStore;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _messageSubject.Subscribe(subscriberName: typeof(MessageConsumer<>).Name,
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

        public async Task ProcessMessage(IMessage message)
        {
            using (var tokenScope = _cancelationTokenStore.CreateScope(message.MessageId))
            {
              await  _messageHandler.Handle(message);
            }
        }
    }
}
