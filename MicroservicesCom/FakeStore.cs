using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersService.Models;

namespace CompaniesService
{
    public class FakeStore
    {
        public List<User> Users { get; set; }

        public FakeStore()
        {
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
    }
}
