using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpecTest
{
    public class PersonRepository : IPersonRepository
    {
        public PersonRepository(SpecContext context)
        {
            Context = context;
        }

        public SpecContext Context { get; }

        public void AddStuff()
        {
            var data = Enumerable.Range(1, 5).Select(index => new Person
            {
                Age = index * 10,
                FirstName = $"FirstName{index}",
                LastName = $"LastName{index}",
                Experierience = index * 1000
            });

            this.Context.Persons.AddRange(data);

            this.Context.SaveChanges();
        }

        public IQueryable<Person> GetPersons()
        {
            return this.Context.Persons;
        }
    }
}
