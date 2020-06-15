using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompaniesService.Messages
{
    public class SubscriptionSuccessfulEvent
    {
        public string CompanyId { get; set; }
        public string UserId { get; set; }
    }
}
