using Domain.Contracts.Repositories;
using Domain.Contracts.Services;
using Domain.Entities;
using Domain.Resources;

namespace Domain.Services.Services
{
    public class BaseService<TEntity, TPrimarykey> : IBaseService<TEntity> where TEntity : BaseEntity<TPrimarykey>
    {
        private readonly IBaseRepository<TEntity> _repository;

        public BaseService(IBaseRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public virtual void Create(TEntity entity)
        {
            _repository.Create(entity);
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity is null)
                throw new InvalidOperationException($"The specified user {ValidationResource.NotExists}");

            _repository.Delete(entity);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _repository.GetAll();
        }

        public TEntity GetById(int id)
        {
            return _repository.GetById(id);
        }

        public virtual void Update(TEntity entity)
        {
            _repository.Update(entity);
        }
    }
}
