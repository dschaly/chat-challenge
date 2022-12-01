using Domain.Entities;

namespace Domain.Contracts.Services
{
    public interface IUserService : IBaseService<User>
    {
        bool IsUserAvailableToEnterTheRoom(string userName);
        bool IsUserOnline(int userId);
        void ToggleUserOnlineStatus(int userId);
    }
}
