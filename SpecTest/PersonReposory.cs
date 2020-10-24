using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpecTest
{
    public class PersonReposory : IPersonRepository
    {
        public IQueryable<Person> GetPersons()
        {
            throw new NotImplementedException();
        }
    }
}
