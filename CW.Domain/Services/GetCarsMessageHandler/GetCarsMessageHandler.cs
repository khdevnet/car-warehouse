using CW.Core.MessageHandler;
using CW.Core.Notifications;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CW.Domain.Services.GetCarsMessageHandler
{
    internal class GetCarsMessageHandler : IMessageHandler<GetCarsMessage>
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly INotificationSender _notificationSender;

        public GetCarsMessageHandler(
            IHttpClientFactory clientFactory,
            ILogger<GetCarsMessageHandler> logger,
           INotificationSender notificationSender)
        {
            _clientFactory = clientFactory;
            _notificationSender = notificationSender;
        }
        public async Task Handle(GetCarsMessage eventMessage)
        {
            var result = await SendHttpRequestAsync(eventMessage);
            await _notificationSender.SendAsync(result);
        }

        public async Task<string> SendHttpRequestAsync(GetCarsMessage message)
        {
            var httpClient = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:7071/api/Function1");

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();

            }
            return null;
        }
    }
}
