using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SpecTest.Specifications
{
    public class OnlyMalesSpecification : Specification<Person>
    {

        public override Expression<Func<Person, bool>> ToExpression()
        {
            return it => it.Gender == Gender.Male;
        }
    }
}
