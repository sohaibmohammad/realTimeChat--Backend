using Chat.Domain.src.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.src.Abstraction
{
	public interface IBaseRepository<TEntity> where TEntity : SharedEntity
	{
		IQueryable<TEntity> Query(bool includeDeleted = false);

		Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

		Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

		Task UpdateAsync(TEntity entity);

		Task DeleteAsync(TEntity entity);

		Task SaveChangesAsync(CancellationToken cancellationToken = default);
		Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default);
		Task GetCountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default);

	}
}
