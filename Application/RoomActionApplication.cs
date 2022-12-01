using Application.Mapper.Response;
using Domain.Contracts.Application;
using Domain.Contracts.Services;
using Domain.DTOs.Request;
using Domain.DTOs.Response;

namespace Application
{
    public sealed class RoomActionApplication : IRoomActionApplication
    {
        private readonly IRoomActionService _roomActionService;

        public RoomActionApplication(IRoomActionService roomActionService)
        {
            _roomActionService = roomActionService;
        }
    
        public void EnterTheRoom(EnterTheRoomRequest request)
        {
            _roomActionService.EnterTheRoom(request);
        }

        public void LeaveTheRoom(LeaveTheRoomRequest request)
        {
            _roomActionService.LeaveTheRoom(request);
        }

        public void Comment(CommentRequest request)
        {
            _roomActionService.Comment(request);
        }

        public void HighFive(HighFiveRequest request)
        {
            _roomActionService.HighFive(request);
        }

        public ICollection<RoomActionResponse> GetAllActions()
        {
            return _roomActionService.GetAllActions();
        }

        public ICollection<ByHourActionResult> GetHistoryByHour(RoomActionsByHourFilter filter)
        {
            return _roomActionService.GetHistoryByHour(filter);
        }

        public ICollection<ByMinuteActionResult> GetHistoryByMinute(RoomActionsByMinuteFilter filter)
        {
            return _roomActionService.GetHistoryByMinute(filter);
        }
    }
}
