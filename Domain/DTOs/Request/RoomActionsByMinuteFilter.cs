using Domain.Entities;
using LinqKit;
using System.Linq.Expressions;

namespace Domain.DTOs.Request
{
    public sealed class RoomActionsByMinuteFilter : BaseFilter<RoomAction>
    {
        public DateTime? InitialDate { get; set; }
        public DateTime? EndDate { get; set; }

        private Expression<Func<RoomAction, bool>> filter = PredicateBuilder.New<RoomAction>(true);

        public override Expression<Func<RoomAction, bool>> GetFilter()
        {
            if (InitialDate.HasValue)
                filter = filter.And(x => x.ActionDate >= InitialDate.Value);

            if (EndDate.HasValue)
                filter = filter.And(x => x.ActionDate <= EndDate.Value);

            return filter;
        }
    }
}
