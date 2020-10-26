using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpecTest.Specifications
{
    public class AgeGreaterThanSpec : BaseSpecification<Person>
    {
        public AgeGreaterThanSpec(int age)
        {
            Criteria = it => it.Age > age;
        }
    }
}
