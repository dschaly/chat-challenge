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

            //if (ShedExitDateEnd.HasValue)
            //{
            //    var exitDate = new DateTime(ShedExitDateEnd.Value.Year, ShedExitDateEnd.Value.Month, ShedExitDateEnd.Value.Day, 23, 59, 59);
            //    filter = filter.And(x => x.Sheds.Any(y => y.ExitDate <= exitDate));
            //}

            return Filter;
        }
    }
}
