namespace Domain.Contracts.Services
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        void Create(TEntity entity);

        void Update(TEntity entity);

        void Delete(int id);

        IEnumerable<TEntity> GetAll();

        TEntity GetById(int id);
        bool Exists(int id);
    }
}
