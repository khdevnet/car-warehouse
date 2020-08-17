using CW.Core.Notifications;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CW.WebApi.Hubs
{
    public class NotificationSender : INotificationSender
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationSender(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendAsync(string json)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", "System", json);
        }
    }
}
