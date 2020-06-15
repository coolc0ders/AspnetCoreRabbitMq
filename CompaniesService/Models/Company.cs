using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompaniesService.Models
{
    public class Company
    {
        public string Name { get; set; }
        public string Domain { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public List<string> FollowersIds { get; set; }
        public string Id { get; set; }

        public Company()
        {
            FollowersIds = new List<string>();
        }
    }
}
