using System.Collections.Generic;
using System.Linq;
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

        [HttpGet("Add")]
        public IActionResult Add()
        {
            this.personRepository.AddStuff();

            return Ok();
        }
    }
}
