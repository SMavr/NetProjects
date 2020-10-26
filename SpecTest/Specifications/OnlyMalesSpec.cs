using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpecTest.Specifications
{
    public class OnlyMalesSpec : BaseSpecification<Person>
    {
        public OnlyMalesSpec()
        {
            Criteria = it => it.Gender == Gender.Male;
        }
    }
}
