using Domain.Entities;

namespace Domain.Contracts.Services
{
    public interface IUserService : IBaseService<User>
    {
        bool Exists(int userId);
        bool IsUserAvailableForEnterTheRoom(string userName);
    }
}
