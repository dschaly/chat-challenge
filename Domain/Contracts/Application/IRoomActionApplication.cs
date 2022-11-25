using Domain.DTOs;

namespace Domain.Contracts.Application
{
    public interface IRoomActionApplication
    {
        ICollection<ByHourActionResult> GetHistoryByHour();
        ICollection<ByMinuteActionResult> GetHistoryByMinute();
    }
}
