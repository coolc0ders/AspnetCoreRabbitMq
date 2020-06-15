using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//TODO: make sure the namespace is the same as that of the message sent by the other service
namespace UsersService.Messages
{
    public class SubscribeToCompanyCommand
    {
        public string CompanyId { get; set; }
        public string UserId { get; set; }
    }
}
