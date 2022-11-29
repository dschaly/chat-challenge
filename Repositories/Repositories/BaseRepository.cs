using Arch.EntityFrameworkCore.UnitOfWork;
using Domain.Contracts.Repositories;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class BaseRepository<TEntity, TPrimarykey> : IBaseRepository<TEntity> where TEntity : BaseEntity<TPrimarykey>
    {
        private readonly IUnitOfWork<DataContext> _context;

        public BaseRepository(IUnitOfWork<DataContext> context)
        {
            _context = context;
        }

        public void Create(TEntity entity)
        {
            try
            {
                _context.DbContext.Set<TEntity>().Add(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(TEntity entity)
        {
            try
            {
                _context.DbContext.Set<TEntity>().Remove(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _context.DbContext.Set<TEntity>().ToList();
        }

        public TEntity GetById(int id)
        {
            return _context.DbContext.Set<TEntity>()
                .FirstOrDefault((TEntity x) => x.Id.Equals(id));
        }

        public void Update(TEntity entity)
        {
            try
            {
                _context.DbContext.Set<TEntity>().Update(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
