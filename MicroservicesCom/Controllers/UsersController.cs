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
        static public List<User> Users { get; set; }

        public UsersController(IOptions<RabbitmqConfig> rabbitConfig,
            ISendEndpointProvider sendEndpoint, IPublishEndpoint publish)
        {
            _rabbitConfig = rabbitConfig.Value;
            _sendEndpoint = sendEndpoint;
            _publishEndPoint = publish;

            Users = new List<User>
            {
                new User
                {
                    Id = "8234f55",
                    Address = "101 Beaker street",
                    Name = "Rebecca Straus",
                    Age = 22,
                    Email = "rebecca@gmail.com",
                    PhoneNumber = "0745986358",
            FollowingCompanyIds = new List<string>()
                },
                new User
                {
                    Id = "34234f55",
                    Address = "201 Beaker street",
                    Name = "Robert Straus",
                    Age = 22,
                    Email = "robert@gmail.com",
                    PhoneNumber = "084566358",
            FollowingCompanyIds = new List<string>()
                },
                new User
                {
                    Id = "7832rf55",
                    Address = "2 Avenue Brule Hotel",
                    Name = "Henri Danger",
                    Age = 40,
                    Email = "henri@outlook.com",
                    PhoneNumber = "07478953358",
            FollowingCompanyIds = new List<string>()
                },
                new User
                {
                    Id = "dert32r532",
                    Address = "4 pont de Russel",
                    Name = "Angelina Black",
                    Age = 24,
                    Email = "angel@gmail.com",
                    PhoneNumber = "0745796358",
            FollowingCompanyIds = new List<string>()
                }
            };
        }

        [HttpPut]
        public async Task<IActionResult> SubscribeToCompany([FromBody] SubscribeBody subscribe)
        {
            var user = Users.SingleOrDefault(u => u.Id == subscribe.UserId);
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
            return Ok(Users);
        }

        [HttpGet("{userId}")]
        public IActionResult GetUser(string userId)
        {
            return Ok(Users.SingleOrDefault(u => u.Id == userId));
        }
    }

    public class SubscribeBody
    {
        public string CompanyId { get; set; }
        public string UserId { get; set; }
    }
}
