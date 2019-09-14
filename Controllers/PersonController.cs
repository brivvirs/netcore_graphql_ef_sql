using graphqlpractise.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace graphqlpractise.Controllers
{
    [Route("person")]
    public class PersonController : Controller
    {
        readonly SeedDatabase _seedDb;
        public PersonController(SeedDatabase seedDb)
        {
            _seedDb = seedDb;
        }

        [HttpGet("add/{name}/{surname}")]
        public async Task<IActionResult> AddPerson(string name, string surname)
        {
            return Ok(await _seedDb.AddPerson(name, surname));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetPerson()
        {
            return Ok(await _seedDb.All());
        }

    }
}
