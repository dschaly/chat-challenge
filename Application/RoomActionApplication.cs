using Application.Mapper.Response;
using AutoMapper;
using Domain.Contracts.Application;
using Domain.Contracts.Services;
using Domain.DTOs.Request;
using Domain.DTOs.Response;
using Domain.Entities;
using Domain.Enums;
using Domain.Resources;

namespace Application
{
    public sealed class RoomActionApplication : IRoomActionApplication
    {
        private readonly IRoomActionService _roomActionService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public RoomActionApplication(IRoomActionService roomActionService, IUserService userService, IMapper mapper)
        {
            _roomActionService = roomActionService;
            _userService = userService;
            _mapper = mapper;
        }
    
        public void Comment(CommentRequest request)
        {
            if (!_userService.IsUserAvailableToComment(request.UserId))
                throw new InvalidOperationException("The specified user can't join the chat room.");

            var entity = new RoomAction
            {
                UserId = request.UserId,
                ActionDate = DateTime.Now,
                ActionId = (int)ActionEnum.COMMENT,
                Comment = new Comment
                {
                    Message = request.Comment,
                    UserId = request.UserId,
                },
                HighFiveId = null,
            };

            _roomActionService.Create(entity);
        }

        public void EnterTheRoom(EnterTheRoomRequest request)
        {
            if (!_userService.IsUserAvailableToEnterTheRoom(request.UserName))
                    throw new InvalidOperationException("The specified user can't join the chat room.");

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

        public void LeaveTheRoom(LeaveTheRoomRequest request)
        {
            if (!_userService.IsUserAvailableToLeaveTheRoom(request.UserId))
                throw new InvalidOperationException(ValidationResource.NotExists);

            var entity = new RoomAction
            {
                UserId = request.UserId,
                ActionDate = DateTime.Now,
                ActionId = (int)ActionEnum.LEAVE_THE_ROOM,
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

        public void HighFive(HighFiveRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
