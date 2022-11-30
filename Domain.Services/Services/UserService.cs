using Domain.Contracts.Repositories;
using Domain.Contracts.Services;
using Domain.Entities;
using Domain.Enums;
using Domain.Resources;

namespace Domain.Services.Services
{
    public sealed class UserService : BaseService<User, int>, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) : base(userRepository)
        {
            _userRepository = userRepository;
        }

        public bool IsUserAvailableToEnterTheRoom(string userName)
        {
            var user = _userRepository.GetUserByUserName(userName);
            
            // User not yet registered
            if (user is null) return true;

            // User already left, so he can re-enter the chat room
            if (user.RoomActions.Any(x => 
                x.ActionId == (int) ActionEnum.LEAVE_THE_ROOM)) return true;

            // User joined but didn't leave, so he can't enter again
            if (user.RoomActions.Any(x =>
                x.ActionId == (int)ActionEnum.ENTER_THE_ROOM &&
                x.ActionId != (int)ActionEnum.LEAVE_THE_ROOM)) return false;

            return false;
        }

        public bool IsUserAvailableToLeaveTheRoom(int userId)
        {
            var user = _userRepository.GetById(userId);

            // User not yet registered
            if (user is null) return false;

            // User has entered but hasn't left, so he can leave the chat room
            if (user.RoomActions.Any(x =>
                x.ActionId == (int)ActionEnum.ENTER_THE_ROOM &&
                x.ActionId != (int)ActionEnum.LEAVE_THE_ROOM)) return true;

            return false;
        }

        public bool IsUserAvailableToComment(int userId)
        {
            var user = _userRepository.GetById(userId);

            // User not registered
            if (user is null) return false;

            // User joined but didn't leave, so he can't enter again
            if (user.RoomActions.Any(x =>
                x.ActionId == (int)ActionEnum.ENTER_THE_ROOM &&
                x.ActionId != (int)ActionEnum.LEAVE_THE_ROOM)) return true;

            // User joined but didn't leave, so he can't enter again
            if (user.RoomActions.Any(x =>
                x.ActionId == (int)ActionEnum.ENTER_THE_ROOM &&
                x.ActionId == (int)ActionEnum.LEAVE_THE_ROOM)) return true;

            return false;
        }

        // Overriding method due EF InMemory failing to validate non-nullable properties
        public override void Create(User entity)
        {
            if (string.IsNullOrEmpty(entity.UserName))
                throw new InvalidOperationException($"UserName {ValidationResource.Informed}");

            base.Create(entity);
        }

        // Overriding method due EF InMemory failing to validate non-nullable properties
        public override void Update(User entity)
        {
            if (string.IsNullOrEmpty(entity.UserName))
                throw new InvalidOperationException($"UserName {ValidationResource.Informed}");

            if (entity.Id <= 0)
                throw new InvalidOperationException($"Id {ValidationResource.Informed}");

            base.Update(entity);
        }
    }
}
