using Chat.Domain.src.Abstraction;
using Chat.Domain.src.Entity;
using Chat.Infrastructure.src.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.src.Repository
{ 
	public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
	{

		private readonly AppDbContext _context;
		private readonly DbSet<TEntity> _dbSet;
		private readonly ILogger<BaseRepository<TEntity>> _logger;

		public BaseRepository(AppDbContext context, ILogger<BaseRepository<TEntity>> logger)
		{
			_context = context;
			_dbSet = _context.Set<TEntity>();
			_logger = logger;
		}

	

		public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
		{
			try
			{
				var entry = await _dbSet.AddAsync(entity, cancellationToken);
				return entry.Entity;
			}
			catch (DbUpdateException ex)
			{
				_logger.LogError(ex, "Error adding entity {Entity}", typeof(TEntity).Name);
				throw;
			}
		}

		public Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}

		public async Task DeleteAsync(TEntity entity)
		{
		var isSoftDelete=typeof(TEntity).GetProperty("IsDeleted")!=null;
			if (isSoftDelete)
			{
				entity.GetType().GetProperty("IsDeleted")?.SetValue(entity, true);
				await UpdateAsync(entity);
			}
			else
			{
				_dbSet.Remove(entity);
			}
		}

		public async Task<TEntity?> GetByIdAsync(Guid Id, CancellationToken cancellationToken = default)
		{
			return await _dbSet.FirstOrDefaultAsync(x => x.id == Id, cancellationToken);
		}

		public Task GetCountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
		{
			if (predicate != null)
			{
				return _dbSet.CountAsync(predicate, cancellationToken);

			}
		
				return _dbSet.CountAsync(cancellationToken);
		}

		public IQueryable<TEntity> Query(bool includeDeleted = false)
		{
			if (typeof(TEntity).GetProperty("IsDeleted")!=null && !includeDeleted)
				return _dbSet.AsNoTracking().Where(e=>!EF.Property<bool>(e,"IsDeleted"));

			return _dbSet.AsNoTracking();
		}

		public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			await _context.SaveChangesAsync(cancellationToken);
		}

		public Task UpdateAsync(TEntity entity)
		{
			try
			{
				_dbSet.Update(entity);
				return Task.CompletedTask;
			}
			catch (Exception ex)
			{
				throw new Exception("Error in update data");
			}
		}
		public async Task<IDbContextTransaction> BeginTransactionAsync()
		{
			return await _context.Database.BeginTransactionAsync();
		}
		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
