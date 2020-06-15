using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersService.Messages
{
    public class SubscribeToCompanyCommand
    {
        public string CompanyId { get; set; }
        public string UserId { get; set; }
    }
}
