using Application.Mapper.Response;
using AutoMapper;
using Domain.Contracts.Application;
using Domain.Contracts.Services;
using Domain.DTOs.Request;
using Domain.DTOs.Response;
using Domain.Entities;
using Domain.Enums;

namespace Application
{
    public sealed class RoomActionApplication : IRoomActionApplication
    {
        private readonly IRoomActionService _roomActionService;
        private readonly IMapper _mapper;

        public RoomActionApplication(IRoomActionService roomActionService, IMapper mapper)
        {
            _roomActionService = roomActionService;
            _mapper = mapper;
        }
    
        public void Comment(int userId, string comment)
        {
            throw new NotImplementedException();
        }

        public void EnterTheRoom(EnterTheRoomRequest request)
        {
            var entity = new RoomAction
            {
                User = new User
                {
                    UserName = request.UserName,
                },
                ActionDate = DateTime.Now,
                ActionId = (int)ActionEnum.ENTER_THE_ROOM,
                CommentId = null,
                HighFiveId = null,
            };

            _roomActionService.Create(entity);
        }

        public ICollection<RoomActionResponse> GetAllActions()
        {
            var roomActions = _roomActionService.GetAll();
            return _mapper.Map<List<RoomActionResponse>>(roomActions);

        }

        public ICollection<ByHourActionResult> GetHistoryByHour()
        {
            throw new NotImplementedException();
        }

        public ICollection<ByMinuteActionResult> GetHistoryByMinute()
        {
            throw new NotImplementedException();
        }

        public void HighFive(int userIdFrom, int userIdTo)
        {
            throw new NotImplementedException();
        }

        public void LeaveTheRoom(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
