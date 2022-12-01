using LinqKit;
using System.Linq.Expressions;

namespace Domain.DTOs
{
    public abstract class BaseFilter<T>
    {
        public Expression<Func<T, bool>> Filter = PredicateBuilder.New<T>(true);

        public virtual Expression<Func<T, bool>> GetFilter()
        {
            return Filter;
        }
    }
}
