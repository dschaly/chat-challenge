using AutoMapper;
using Domain.Contracts.Repositories;
using Domain.Contracts.Services;
using Domain.DTOs.Response;
using Domain.Entities;
using Domain.Resources;

namespace Domain.Services.Services
{
    public sealed class UserService : BaseService<User, int>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper) : base(userRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }


        public bool IsUserAvailableToEnterTheRoom(string userName)
        {
            var user = _userRepository.GetUserByUserName(userName);
            
            // User not yet registered
            if (user is null) return true;

            // User is not in the chat room, so he can come back
            if (!user.IsOnline) return true;

            return false;
        }

        public bool IsUserOnline(int userId)
        {
            var user = _userRepository.GetById(userId);

            // User not yet registered
            if (user is null) return false;

            // User is in the chat room
            if (user.IsOnline) return true;

            return false;
        }

        // Overriding method due EF InMemory failing to validate non-nullable properties
        public override void Create(User entity)
        {
            if (string.IsNullOrEmpty(entity.UserName))
                throw new InvalidOperationException($"UserName {ValidationResource.Informed}");

            base.Create(entity);
        }

        public void ToggleUserOnlineStatus(int userId)
        {
            var user = _userRepository.GetById(userId);

            if (user is null)
                throw new InvalidOperationException($"User {ValidationResource.NotExists}");

            user.IsOnline = !user.IsOnline;
            base.Update(user);
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

        public ICollection<UserResponse> GetAllUsers()
        {
            var users = _userRepository.GetAll();
            return _mapper.Map<List<UserResponse>>(users);
        }
    }
}
