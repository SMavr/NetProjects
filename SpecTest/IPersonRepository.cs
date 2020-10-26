using SpecTest.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SpecTest
{
    public interface IPersonRepository
    {
        IQueryable<Person> GetPersons();

        void AddStuff();

        IReadOnlyList<Person> Find(ISpecification<Person> specification);
    }
}
