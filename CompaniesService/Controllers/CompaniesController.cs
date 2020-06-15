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
        FakeStore _store;

        public CompaniesController(FakeStore store)
        {
            _store = store;
        }

        [HttpGet]
        public IActionResult GetCompanies()
        {
            return Ok(_store.Companies);
        }

        [HttpGet("{companyId}")]
        public IActionResult GetCompany(string companyId)
        {
            var company = _store.Companies.SingleOrDefault(c => c.Id == companyId);
            return Ok(company);
        }
    }
}
