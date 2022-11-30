using Domain.Contracts.Repositories;
using Domain.Contracts.Services;
using Domain.Entities;
using Domain.Resources;

namespace Domain.Services.Services
{
    public sealed class RoomActionService : BaseService<RoomAction, int>, IRoomActionService
    {
        private readonly IRoomActionRepository _repository;

        public RoomActionService(IRoomActionRepository repository) : base(repository)
        {
            _repository = repository;
        }

        // Overriding method due EF InMemory failing to validate non-nullable properties
        public override void Create(RoomAction entity)
        {
            if (string.IsNullOrEmpty(entity.User.UserName))
                throw new InvalidOperationException($"UserName {ValidationResource.Informed}");

            base.Create(entity);
        }

        // Overriding method due EF InMemory failing to validate non-nullable properties
        public override void Update(RoomAction entity)
        {
            if (entity.Id <= 0)
                throw new InvalidOperationException($"Id {ValidationResource.Informed}");

            base.Update(entity);
        }
    }
}
