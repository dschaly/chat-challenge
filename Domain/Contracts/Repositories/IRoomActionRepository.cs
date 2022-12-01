using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using Domain.DTOs;
using Domain.Entities;

namespace Domain.Contracts.Repositories
{
    public interface IRoomActionRepository : IBaseRepository<RoomAction>
    {
        IPagedList<RoomAction> Search(BaseFilter<RoomAction> filter);
    }
}
