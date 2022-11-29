using Domain.Contracts.Repositories;
using Domain.Contracts.Services;
using Domain.Entities;

namespace Domain.Services.Services
{
    public sealed class RoomActionService : BaseService<RoomAction, int>, IRoomActionService
    {
        private readonly IRoomActionRepository _repository;

        public RoomActionService(IRoomActionRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
