using Application.Mapper.Response;
using Domain.DTOs.Request;
using Domain.DTOs.Response;

namespace Domain.Contracts.Application
{
    public interface IRoomActionApplication
    {
        void EnterTheRoom(EnterTheRoomRequest request);
        void LeaveTheRoom(int userId);
        void Comment(int userId, string comment);
        void HighFive(int userIdFrom, int userIdTo);
        ICollection<ByHourActionResult> GetHistoryByHour();
        ICollection<ByMinuteActionResult> GetHistoryByMinute();
        ICollection<RoomActionResponse> GetAllActions();
    }
}
