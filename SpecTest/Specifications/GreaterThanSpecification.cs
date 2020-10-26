using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SpecTest.Specifications
{
    public class GreaterThanSpecification : Specification<Person>
    {

        public GreaterThanSpecification(int age)
        {
            Age = age;
        }

        public int Age { get; }

        public override Expression<Func<Person, bool>> ToExpression()
        {
            return it => it.Age > this.Age;
        }
    }
}
