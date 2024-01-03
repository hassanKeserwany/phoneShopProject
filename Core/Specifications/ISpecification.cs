using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public interface ISpecification<T>
    {

        Expression<Func<T,bool>> Criteria { get; } // get something by some criteria
        List<Expression<Func<T,object>>> Includes { get;} // include nav property
    }
}
