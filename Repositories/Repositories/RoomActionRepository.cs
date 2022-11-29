using Arch.EntityFrameworkCore.UnitOfWork;
using Domain.Contracts.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RoomActionRepository : BaseRepository<RoomAction, int>, IRoomActionRepository
    {
        private protected IUnitOfWork<DataContext> _context { get; set; }

        public RoomActionRepository(IUnitOfWork<DataContext> context) : base(context)
        {
            _context = context;

        }

        public override ICollection<RoomAction> GetAll()
        {
            var roomActions = _context.DbContext.RoomActions
                .Include(x => x.User)
                .Include(x => x.Comment)
                .Include(x => x.HighFive)
                .ToList();

            return roomActions;
        }
    }
}
