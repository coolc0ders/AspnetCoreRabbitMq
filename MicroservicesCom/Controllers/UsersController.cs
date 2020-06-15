using CompaniesService;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersService.Helpers;
using UsersService.Messages;
using UsersService.Models;

namespace UsersService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        RabbitmqConfig _rabbitConfig;
        ISendEndpointProvider _sendEndpoint;
        IPublishEndpoint _publishEndPoint;
        FakeStore _fakeStore;

        public UsersController(IOptions<RabbitmqConfig> rabbitConfig,
            ISendEndpointProvider sendEndpoint, IPublishEndpoint publish, FakeStore fakeStore)
        {
            _fakeStore = fakeStore;
            _rabbitConfig = rabbitConfig.Value;
            _sendEndpoint = sendEndpoint;
            _publishEndPoint = publish;
        }

        [HttpPut]
        public async Task<IActionResult> SubscribeToCompany([FromBody] SubscribeBody subscribe)
        {
            var user = _fakeStore.Users.SingleOrDefault(u => u.Id == subscribe.UserId);
            user.FollowingCompanyIds.Add(subscribe.CompanyId);
            await SendMessage(new SubscribeToCompanyCommand
            {
                UserId = subscribe.UserId,
                CompanyId = subscribe.CompanyId
            }, "companiesService");
            return Ok(user);
        }

        public async Task SendMessage<T>(T message, string targetEndPoint)
        {
            var endpoint = $"rabbitmq://{_rabbitConfig.Host}:{_rabbitConfig.Port}/{targetEndPoint}?durable={_rabbitConfig.DurableQueue}";
            var finalEndpoint = await _sendEndpoint.GetSendEndpoint(new Uri(endpoint));
            await finalEndpoint.Send(message);
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(_fakeStore.Users);
        }

        [HttpGet("{userId}")]
        public IActionResult GetUser(string userId)
        {
            return Ok(_fakeStore.Users.SingleOrDefault(u => u.Id == userId));
        }
    }

    public class SubscribeBody
    {
        public string CompanyId { get; set; }
        public string UserId { get; set; }
    }
}
