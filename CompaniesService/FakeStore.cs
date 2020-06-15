using CompaniesService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompaniesService
{
    public class FakeStore
    {
        public List<Company> Companies { get; set; }

        public FakeStore()
        {
            Companies = new List<Company>
            {
                new Company
                {
                    Id = "4563r5",
                    Address = "La defense",
                    Country = "France",
                    Domain = "Tech",
                    Name = "Global Tech"
                },
                new Company
                {
                    Id = "iorep",
                    Address = "Issy les moulineux",
                    Country = "France",
                    Domain = "Tech",
                    Name = "Tech for Tech"
                },
            };
        }
    }
}
