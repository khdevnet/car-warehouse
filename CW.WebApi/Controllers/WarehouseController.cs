using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CW.Core.Abstractions.MessageHandler;
using CW.Core.MessageHandler;
using CW.Domain;
using CW.Domain.Services.GetCarsMessageHandler;
using CW.WebApi.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CW.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WarehouseController : ControllerBase
    {
        private readonly ILogger<WarehouseController> _logger;
        private readonly IMessageObservable<GetCarsMessage> _messageSubject;
        private readonly IHubContext<NotificationHub> _hubContext;

        public WarehouseController(
            ILogger<WarehouseController> logger,
            IMessageObservable<GetCarsMessage> messageSubject,
            IHubContext<NotificationHub> hubContext)
        {
            _logger = logger;
            _messageSubject = messageSubject;
            _hubContext = hubContext;
        }

        [HttpGet]
        [Route("cars")]
        public IEnumerable<Guid> GetCars()
        {
            var message = new GetCarsMessage(Guid.NewGuid(), DateTime.Now);
            _messageSubject.Publish(message);
            return new Guid[] { message.MessageId };
        }
    }
}
