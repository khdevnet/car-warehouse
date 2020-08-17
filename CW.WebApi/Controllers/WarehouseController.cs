using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CW.Core.Abstractions.MessageHandler;
using CW.Core.MessageHandler;
using CW.Domain;
using CW.Domain.Services.GetCarsMessageHandler;
using CW.WebApi.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CW.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WarehouseController : ControllerBase
    {
        private readonly IMessageObservable<GetCarsMessage> _messageSubject;

        public WarehouseController(
            IMessageObservable<GetCarsMessage> messageSubject)
        {
            _messageSubject = messageSubject;
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
