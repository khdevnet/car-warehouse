using CW.Core.Notifications;
using CW.WebApi.Hubs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CW.WebApi
{
    public static class WebApiDiModule
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<INotificationSender, NotificationSender>();
        }
    }
}
