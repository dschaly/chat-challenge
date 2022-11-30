using Domain.Entities;

namespace Domain.Contracts.Services
{
    public interface IUserService : IBaseService<User>
    {
        bool IsUserAvailableToEnterTheRoom(string userName);
        bool IsUserAvailableToLeaveTheRoom(int userId);
        bool IsUserAvailableToComment(int userId);
    }
}
