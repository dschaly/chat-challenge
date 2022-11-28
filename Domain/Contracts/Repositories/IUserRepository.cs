using Domain.Entities;

namespace Domain.Contracts.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        bool Exists(int userId);
        User GetUserByUserName(string userName);
    }
}
