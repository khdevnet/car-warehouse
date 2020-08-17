using CW.Core.Abstractions.MessageHandler;
using CW.Core.Abstractions.MessageHandler.Dto;
using CW.Core.MessageHandler;
using Microsoft.Extensions.DependencyInjection;

namespace CW.Core
{
    public static class CoreDiModule
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<CancelationTokenSourceProvider>();
        }

        public static void ConfigureMessageConsumer<TMessage>(this IServiceCollection services) where TMessage : IMessage
        {
            services.AddSingleton<IMessageObservable<TMessage>, MessageObservable<TMessage>>();
            services.AddHostedService<MessageConsumer<TMessage>>();
        }
    }
}
