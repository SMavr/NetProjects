using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SpecTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonRepository personRepository;

        public PersonController(ILogger<PersonController> logger, IPersonRepository personRepository)
        {
            _logger = logger;
            this.personRepository = personRepository;
        }

        [HttpGet]
        public IEnumerable<Person> Get()
        {
            return this.personRepository.GetPersons().ToList();
        }

        [HttpPost("Add")]
        public IActionResult Add()
        {
            this.personRepository.AddStuff();

            return Ok();
        }

        [HttpGet("Filter")]
        public IEnumerable<Person> FilterMales()
        {
            Expression<Func<Person, bool>> expression = it => it.Gender == Gender.Male;
            return this.personRepository.Find(expression); // Getting a list of movies
        }
    }
}
