using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersService.Messages;
using Microsoft.Extensions.Options;
using CompaniesService.Helpers;
using CompaniesService.Controllers;
using CompaniesService.Messages;

namespace CompaniesService.Consumers
{
    public class SubscribeToCompanyConsumer : IConsumer<SubscribeToCompanyCommand>
    {
        RabbitmqConfig _rabbitConfig;
        ISendEndpointProvider _sendEndpoint;
        IPublishEndpoint _publishEndPoint;
        FakeStore _store;

        public SubscribeToCompanyConsumer(IOptions<RabbitmqConfig> rabbitConfig, 
            ISendEndpointProvider sendEndpoint, IPublishEndpoint publish,
            FakeStore store)
        {
            _store = store;
            _rabbitConfig = rabbitConfig.Value;
            _sendEndpoint = sendEndpoint;
            _publishEndPoint = publish;
        }

        public Task Consume(ConsumeContext<SubscribeToCompanyCommand> context)
        {
            try
            {
                var company = _store.Companies.SingleOrDefault(c => c.Id == context.Message.CompanyId);
                var f = company.FollowersIds;
                company.FollowersIds.Add(context.Message.UserId);

                return _publishEndPoint.Publish(new SubscriptionSuccessfulEvent
                {
                    CompanyId = context.Message.CompanyId,
                    UserId = context.Message.UserId
                });
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
