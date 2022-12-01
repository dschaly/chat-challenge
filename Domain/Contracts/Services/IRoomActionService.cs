using Application.Mapper.Response;
using Domain.DTOs.Request;
using Domain.DTOs.Response;
using Domain.Entities;

namespace Domain.Contracts.Services
{
    public interface IRoomActionService : IBaseService<RoomAction>
    {
        void EnterTheRoom(EnterTheRoomRequest request);
        void LeaveTheRoom(LeaveTheRoomRequest request);
        void Comment(CommentRequest request);
        void HighFive(HighFiveRequest request);
        ICollection<RoomActionResponse> GetAllActions();
        ICollection<ByHourActionResult> GetHistoryByHour(RoomActionsByHourFilter filter);
        ICollection<ByMinuteActionResult> GetHistoryByMinute(RoomActionsByMinuteFilter filter);
    }
}
