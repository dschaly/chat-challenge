using Application.Mapper.Response;
using AutoMapper;
using Domain.Contracts.Repositories;
using Domain.Contracts.Services;
using Domain.DTOs.Request;
using Domain.DTOs.Response;
using Domain.Entities;
using Domain.Enums;
using Domain.Resources;

namespace Domain.Services.Services
{
    public sealed class RoomActionService : BaseService<RoomAction, int>, IRoomActionService
    {
        private readonly IRoomActionRepository _repository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public RoomActionService(IRoomActionRepository repository, IUserService userService, IMapper mapper) : base(repository)
        {
            _repository = repository;
            _userService = userService;
            _mapper = mapper;
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
                    IsOnline = true
                },
                ActionDate = DateTime.Now,
                ActionId = (int)ActionEnum.ENTER_THE_ROOM,
                CommentId = null,
                HighFiveId = null,
            };

            Create(entity);
        }

        public void LeaveTheRoom(LeaveTheRoomRequest request)
        {
            if (!_userService.IsUserOnline(request.UserId))
                throw new InvalidOperationException($"User {ValidationResource.NotExists}");

            var entity = new RoomAction
            {
                UserId = request.UserId,
                ActionDate = DateTime.Now,
                ActionId = (int)ActionEnum.LEAVE_THE_ROOM,
                CommentId = null,
                HighFiveId = null,
            };

            _userService.ToggleUserOnlineStatus(entity.UserId);
            Create(entity);
        }

        public void Comment(CommentRequest request)
        {
            if (!_userService.IsUserOnline(request.UserId))
                throw new InvalidOperationException("The specified user can't post comments.");

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

            Create(entity);
        }

        public void HighFive(HighFiveRequest request)
        {
            throw new NotImplementedException();
        }

        public ICollection<RoomActionResponse> GetAllActions()
        {
            var roomActions = GetAll();
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

        // Overriding method due EF InMemory failing to validate non-nullable properties
        public override void Update(RoomAction entity)
        {
            if (entity.Id <= 0)
                throw new InvalidOperationException($"Id {ValidationResource.Informed}");

            base.Update(entity);
        }
    }
}
