using Domain.Contracts.Repositories;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class RoomActionRepository : BaseRepository<RoomAction>, IRoomActionRepository
    {
        private readonly DataContext _context;

        public RoomActionRepository(DataContext context) : base(context)
        {
            _context = context; 
        }
    }
}
