using SpecTest.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            var random = new Random();
            Array values = Enum.GetValues(typeof(Gender));
            var data = Enumerable.Range(1, 5).Select(index => new Person
            {
                Age = index * 10,
                FirstName = $"FirstName{index}",
                LastName = $"LastName{index}",
                Experierience = index * 1000,
                Gender = (Gender)values.GetValue(random.Next(values.Length))
        });

            this.Context.Persons.AddRange(data);

            this.Context.SaveChanges();
        }

        public IReadOnlyList<Person> Find(Specification<Person> specification)
        {
            return this.Context.Persons.Select(specification.ToExpression())
                .AsQueryable()
                .ToList();
        }

        public IQueryable<Person> GetPersons()
        {
            return this.Context.Persons;
        }

    }
}
