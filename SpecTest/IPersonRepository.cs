using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpecTest
{
    public interface IPersonRepository
    {
        IQueryable<Person> GetPersons();

        void AddStuff();
    }
}
