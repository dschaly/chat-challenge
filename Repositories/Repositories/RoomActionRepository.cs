using Arch.EntityFrameworkCore.UnitOfWork;
using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using Domain.Contracts.Repositories;
using Domain.DTOs;
using Domain.DTOs.Request;
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
            var roomActions = _context.GetRepository<RoomAction>()
                .GetPagedList(
                    include: source =>
                        source.Include(y => y.User)
                              .Include(y => y.Comment)
                              .Include(y => y.HighFive));

            return roomActions.Items;
        }

        public IPagedList<RoomAction> Search(BaseFilter<RoomAction> filter)
        {
            var response = _context.GetRepository<RoomAction>()
                .GetPagedList(
                    predicate: filter.GetFilter(),
                    pageIndex: 0,
                    pageSize: int.MaxValue,
                    include: source =>
                        source.Include(x => x.User)
                              .Include(x => x.Comment)
                              .Include(x => x.HighFive));
            return response;
        }
    }
}
