using Application.Mapper.Response;
using Domain.DTOs.Request;
using Domain.DTOs.Response;

namespace Domain.Contracts.Application
{
    public interface IRoomActionApplication
    {
        void EnterTheRoom(EnterTheRoomRequest request);
        void LeaveTheRoom(LeaveTheRoomRequest request);
        void Comment(CommentRequest request);
        void HighFive(HighFiveRequest request);
        ICollection<ByHourActionResult> GetHistoryByHour();
        ICollection<ByMinuteActionResult> GetHistoryByMinute();
        ICollection<RoomActionResponse> GetAllActions();
    }
}
