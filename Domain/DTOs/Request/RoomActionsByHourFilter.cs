using Domain.Entities;
using LinqKit;
using System.Linq.Expressions;

namespace Domain.DTOs.Request
{
    public sealed class RoomActionsByHourFilter : BaseFilter<RoomAction>
    {
        public DateTime? InitialDate { get; set; }
        public DateTime? EndDate { get; set; }

        public override Expression<Func<RoomAction, bool>> GetFilter()
        {
            if (InitialDate.HasValue)
                Filter = Filter.And(x => x.ActionDate >= InitialDate.Value);

            if (EndDate.HasValue)
                Filter = Filter.And(x => x.ActionDate <= EndDate.Value);

            return Filter;
        }
    }
}
