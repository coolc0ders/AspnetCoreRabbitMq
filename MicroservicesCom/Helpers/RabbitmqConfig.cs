using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersService.Helpers
{
    /// <summary>
    /// Rabbitmq config for masstransit read more about it here: https://masstransit-project.com/usage/transports/rabbitmq.html#cloudamqp
    /// </summary>
    public class RabbitmqConfig
    {
        public string Host { get; set; }
        public ushort Port { get; set; }
        public string VirtualHost { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool PublisherConfirmation { get; set; }
        public IEnumerable<string> ClusterMembers { get; set; }
        public bool PurgeOnStartup { get; set; }
        public ushort PrefetchCount { get; set; }
        /// <summary>
        /// The queue endpoint for the current service
        /// </summary>
        public string Endpoint { get; set; }
        public bool DurableQueue { get; set; }
    }
}
