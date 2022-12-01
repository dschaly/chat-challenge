using Domain.Entities;

namespace Domain.Contracts.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User GetUserByUserName(string userName);
    }
}
