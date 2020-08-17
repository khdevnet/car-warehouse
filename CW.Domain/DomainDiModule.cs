using CW.Core.Abstractions.MessageHandler;
using CW.Domain.Services.GetCarsMessageHandler;
using Microsoft.Extensions.DependencyInjection;

namespace CW.Domain
{
    public static class DomainDiModule
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<IMessageHandler<GetCarsMessage>, GetCarsMessageHandler>();
        }
    }
}
