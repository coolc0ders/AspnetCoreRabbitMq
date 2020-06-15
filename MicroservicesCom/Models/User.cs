using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersService.Models
{
    public class User
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public short Age { get; set; }
        public List<string> FollowingCompanyIds { get; set; }
        public string Id { get; set; }

        public User()
        {
        }
    }
}
