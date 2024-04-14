using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ReferigenatorSvc.dbcontext
{
	public interface IRepository<TEntity> where TEntity : class
	{
		Task Add(TEntity entity);
		Task AddRange(IEnumerable<TEntity> entities);
		void Update(TEntity entity);
		void UpdateRange(IEnumerable<TEntity> entities);
		void Remove(TEntity entity);
		void RemoveRange(IEnumerable<TEntity> entities);
		Task<int> Count();
		IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
		Task<TEntity> GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate);
		Task<TEntity> Get(int id);

		Task BeginTransaction();
		Task CommitTransaction();
		Task RoleBack();
		Task<IEnumerable<TEntity>> GetAll();
	}
}
