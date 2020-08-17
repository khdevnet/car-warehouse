using CW.Core.MessageHandler;
using CW.Domain.Services.GetCarsMessageHandler;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
