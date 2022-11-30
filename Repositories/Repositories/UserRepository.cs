using Arch.EntityFrameworkCore.UnitOfWork;
using Domain.Contracts.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public sealed class UserRepository : BaseRepository<User, int>, IUserRepository
    {
        private readonly IUnitOfWork<DataContext> _context;
        public UserRepository(IUnitOfWork<DataContext> context) : base(context)
        {
            _context = context;
        }

        public bool Exists(int userId)
        {
            return _context.DbContext.Users.Any(x => x.Id == userId);
        }

        public User GetUserByUserName(string userName)
        {
            return _context.DbContext.Users
                .Include(x => x.RoomActions)
                .Include(x => x.Comments)
                .FirstOrDefault(x => x.UserName == userName);
        }

        public override ICollection<User> GetAll()
        {
            //var roomActions = _context.DbContext.RoomActions
            //    .Include(x => x.User)
            //    .Include(x => x.Comment)
            //    .Include(x => x.HighFive)
            //    .ToList();

            var roomActions = _context.GetRepository<User>()
                .GetPagedList(
                    include: source =>
                        source.Include(y => y.RoomActions)
                              .Include(y => y.Comments)
                              .Include(y => y.HighFives));

            return roomActions.Items;
        }

    }
}
