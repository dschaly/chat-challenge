using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public sealed class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public bool Exists(int userId)
        {
            return _context.Users.Any(x => x.Id == userId);
        }

        public User GetUserByUserName(string userName)
        {
            return _context.Users
                .Include(x => x.RoomActions)
                .Include(x => x.Comments)
                .FirstOrDefault(x => x.UserName == userName);
        }
    }
}
