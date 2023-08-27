using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReferigenatorSvc.dbcontext
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _entities;
        private Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction _transaction;


        public Repository(DbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public virtual async Task Add(TEntity entity)
        {
            await _entities.AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<TEntity> entities)
        {
           await _entities.AddRangeAsync(entities);
        }

        public Task<int> Count()
        {
            return _entities.CountAsync();
        }

        public IEnumerable<TEntity> Find(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Where(predicate);
        }

        public async Task<TEntity> Get(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        public Task<TEntity> GetFirstOrDefault(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.FirstOrDefaultAsync(predicate);
        }

        public void Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _entities.RemoveRange(entities);
        }

        public void Update(TEntity entity)
        {
            _entities.Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _entities.UpdateRange(entities);
        }
        public async Task BeginTransaction()
        {
             _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransaction()
        {
           await _transaction.CommitAsync();
        }

        public async Task  RoleBack()
        {
            await _transaction.RollbackAsync();
        }
    }
}
