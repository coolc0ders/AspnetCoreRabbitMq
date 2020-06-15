using UsersService.Messages;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CompaniesService.Messages;

namespace UsersService.Consumers
{
    public class SubscriptionSuccessfulConsumer : IConsumer<SubscriptionSuccessfulEvent>
    {
        public Task Consume(ConsumeContext<SubscriptionSuccessfulEvent> context)
        {
            Debug.WriteLine("Subscribed successfully");
            return Task.CompletedTask;
        }
    }
}
