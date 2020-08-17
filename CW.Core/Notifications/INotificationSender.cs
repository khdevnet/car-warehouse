using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CW.Core.Notifications
{
    public interface INotificationSender
    {
        Task SendAsync(string json);
    }
}
