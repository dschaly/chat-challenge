using Domain.Contracts.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RoomActionRepository : BaseRepository<RoomAction>, IRoomActionRepository
    {
        private readonly DataContext _context;

        public RoomActionRepository(DataContext context) : base(context)
        {
            _context = context;

        }

        public override ICollection<RoomAction> GetAll()
        {
            var roomActions = _context.RoomActions
                .Include(x => x.User)
                .Include(x => x.Comment)
                .Include(x => x.HighFive)
                .ToList();

            return roomActions;
        }
    }
}
