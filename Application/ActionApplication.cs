using Domain.Contracts.Application;
using Domain.DTOs;

namespace Application
{
    public sealed class RoomActionApplication : IRoomActionApplication
    {
        public ICollection<ByHourActionResult> GetHistoryByHour()
        {
            throw new NotImplementedException();
        }

        public ICollection<ByMinuteActionResult> GetHistoryByMinute()
        {
            throw new NotImplementedException();
        }
    }
}
