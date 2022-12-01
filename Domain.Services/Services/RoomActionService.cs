using Application.Mapper.Response;
using AutoMapper;
using Domain.Contracts.Repositories;
using Domain.Contracts.Services;
using Domain.DTOs.Request;
using Domain.DTOs.Response;
using Domain.Entities;
using Domain.Enums;
using Domain.Resources;
using System.Linq;

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
            };

            _userService.ToggleUserOnlineStatus(entity.UserId);
            Create(entity);
        }

        public void Comment(CommentRequest request)
        {
            if (!_userService.IsUserOnline(request.UserId))
                throw new InvalidOperationException(ValidationResource.UserCantComment);

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
            };

            Create(entity);
        }

        public void HighFive(HighFiveRequest request)
        {
            if (!_userService.Exists(request.UserIdFrom))
                throw new InvalidOperationException(ValidationResource.UserOffline);

            if (!_userService.IsUserOnline(request.UserIdTo))
                throw new InvalidOperationException(ValidationResource.UserOffline);

            var userNameTo = _userService.GetById(request.UserIdTo).UserName;

            var entity = new RoomAction
            {
                UserId = request.UserIdFrom,
                ActionDate = DateTime.Now,
                ActionId = (int)ActionEnum.HIGH_FIVE,
                HighFive = new HighFive
                {
                    UserIdTo = request.UserIdTo,
                    UserNameTo = userNameTo,
                }
            };

            Create(entity);
        }

        public ICollection<RoomActionResponse> GetAllActions()
        {
            var roomActions = GetAll();
            return _mapper.Map<List<RoomActionResponse>>(roomActions);
        }
        public ICollection<ByHourActionResult> GetHistoryByHour(RoomActionsByHourFilter filter)
        {
            var roomActions = _repository.Search(filter).Items;

            var result = roomActions.GroupBy(action => new
            {
                action.ActionDate.Year,
                action.ActionDate.Month,
                action.ActionDate.Day,
                action.ActionDate.Hour
            })
            .Select(actionGroup => new ByHourActionResult
            {
                HourPeriod = new DateTime(actionGroup.Key.Year, actionGroup.Key.Month, actionGroup.Key.Day, actionGroup.Key.Hour, 0, 0),
                EnteredPeopleCount = actionGroup.Count(x => x.ActionId == (int)ActionEnum.ENTER_THE_ROOM),
                LeftPeopleCount = actionGroup.Count(x => x.ActionId == (int)ActionEnum.LEAVE_THE_ROOM),
                CommentCount = actionGroup.Count(x => x.ActionId == (int)ActionEnum.COMMENT),
                HighFivedFromPeopleCount = actionGroup.Where(x => x.HighFiveId.HasValue).GroupBy(x => x.UserId).Count(),
                HighFivedToPeopleCount = actionGroup.Where(x => x.HighFiveId.HasValue).GroupBy(x => x.HighFive!.UserIdTo).Count(),
            })
            .ToList();

            return result;
        }

        public ICollection<ByMinuteActionResult> GetHistoryByMinute(RoomActionsByMinuteFilter filter)
        {
            var roomActions = _repository.Search(filter).Items;

            var result = roomActions.Select(action => new ByMinuteActionResult
            {
                Id = action.Id,
                UserName = action.User.UserName,
                ActionDate = action.ActionDate,
                ActionId = action.ActionId,
                Comment = action.Comment?.Message,
                HighFiveToName = action.HighFive?.UserNameTo
            }).ToList();

            return result;
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
