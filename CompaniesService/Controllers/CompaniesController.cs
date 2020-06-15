using CompaniesService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompaniesService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        public static List<Company> Companies { get; set; }

        public CompaniesController()
        {
            Companies = new List<Company>
            {
                new Company
                {
                    Id = "asdkjf",
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

        [HttpGet]
        public IActionResult GetCompanies()
        {
            return Ok(Companies);
        }

        [HttpGet("{companyId}")]
        public IActionResult GetCompany(string companyId)
        {
            var company = Companies.SingleOrDefault(c => c.Id == companyId);
            return Ok(company);
        }
    }
}
