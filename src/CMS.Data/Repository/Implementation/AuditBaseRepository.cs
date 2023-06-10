using CMS.Data.Model.Entities.Base;
using FreeSql;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.Repository.Implementation
{
	public abstract class AuditBaseRepository<TEntity, TKey> : BaseRepository<TEntity, TKey>, IAuditBaseRepository<TEntity, TKey>
		where TEntity : class
		where TKey : IEquatable<TKey>
	{

		/// <summary>
		/// 是否开户软删除审计
		/// </summary>
		protected readonly bool IsDeleteAudit;


		protected AuditBaseRepository(IFreeSql fsql) : base(fsql, null, null)
		{
		}
		protected AuditBaseRepository(IFreeSql fsql, Expression<Func<TEntity, bool>> filter) : base(fsql, filter, null)
		{
		}

		protected AuditBaseRepository(IFreeSql fsql, UnitOfWorkManager uowManger) : base(uowManger?.Orm ?? fsql, null, null)
		{
			uowManger?.Binding(this);
		}

		protected void BeforeInsert(TEntity entity)
		{
			if (entity is BaseEntity<TKey> baseEntity)
			{
				baseEntity.CreateTime = DateTime.Now;
			}

			BeforeUpdate(entity);
		}

		protected void BeforeUpdate(TEntity entity)
		{

			if (entity is BaseEntity<TKey> baseEntity)
			{
				baseEntity.UpdateTime = DateTime.Now;

			}
		}

		protected void BeforeDelete(TEntity entity)
		{
			if (!IsDeleteAudit) return;
			if (entity is ISoftDelete softDelete)
			{
				softDelete.IsDeleted = true;
			}
		}
		#region Insert

		public override TEntity Insert(TEntity entity)
		{
			BeforeInsert(entity);
			return base.Insert(entity);
		}

		public override Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
		{
			BeforeInsert(entity);
			return base.InsertAsync(entity, cancellationToken);
		}

		public override List<TEntity> Insert(IEnumerable<TEntity> entities)
		{
			foreach (TEntity entity in entities)
			{
				BeforeInsert(entity);
			}

			return base.Insert(entities);
		}

		public override Task<List<TEntity>> InsertAsync(IEnumerable<TEntity> entities,
			CancellationToken cancellationToken = default)
		{
			foreach (TEntity entity in entities)
			{
				BeforeInsert(entity);
			}

			return base.InsertAsync(entities, cancellationToken);
		}

		#endregion

		#region Update

		public override int Update(TEntity entity)
		{
			BeforeUpdate(entity);
			return base.Update(entity);
		}

		public override Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
		{
			BeforeUpdate(entity);
			return base.UpdateAsync(entity, cancellationToken);
		}

		public override int Update(IEnumerable<TEntity> entities)
		{
			foreach (var entity in entities)
			{
				BeforeUpdate(entity);
			}

			return base.Update(entities);
		}

		public override Task<int> UpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
		{
			foreach (var entity in entities)
			{
				BeforeUpdate(entity);
			}

			return base.UpdateAsync(entities, cancellationToken);
		}

		#endregion

		#region Delete

		public override int Delete(TKey id)
		{
			if (!IsDeleteAudit) return base.Delete(id);
			TEntity entity = Get(id);
			BeforeDelete(entity);
			return base.Update(entity);
		}

		public override int Delete(TEntity entity)
		{
			if (!IsDeleteAudit) return base.Delete(entity);
			Attach(entity);
			BeforeDelete(entity);
			return base.Update(entity);
		}

		public override int Delete(IEnumerable<TEntity> entities)
		{
			if (!entities.Any() || !IsDeleteAudit) return base.Delete(entities);
			Attach(entities);
			foreach (TEntity entity in entities)
			{
				BeforeDelete(entity);
			}
			return base.Update(entities);
		}

		public override async Task<int> DeleteAsync(TKey id, CancellationToken cancellationToken = default)
		{
			if (!IsDeleteAudit) return await base.DeleteAsync(id, cancellationToken);
			TEntity entity = await GetAsync(id, cancellationToken);
			BeforeDelete(entity);
			return await base.UpdateAsync(entity, cancellationToken);
		}

		public override Task<int> DeleteAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
		{
			if (!entities.Any() || !IsDeleteAudit) return base.DeleteAsync(entities, cancellationToken);
			Attach(entities);
			foreach (TEntity entity in entities)
			{
				BeforeDelete(entity);
			}

			return base.UpdateAsync(entities, cancellationToken);
		}

		public override Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
		{
			if (!IsDeleteAudit) return base.DeleteAsync(entity, cancellationToken);
			Attach(entity);
			BeforeDelete(entity);
			return base.UpdateAsync(entity, cancellationToken);
		}

		public override int Delete(Expression<Func<TEntity, bool>> predicate)
		{
			if (!IsDeleteAudit) return base.Delete(predicate);
			List<TEntity> items = base.Select.Where(predicate).ToList();
			if (items.Count == 0)
			{
				return 0;
			}

			foreach (var entity in items)
			{
				BeforeDelete(entity);
			}

			return base.Update(items);
		}

		public override async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate,
			CancellationToken cancellationToken = default)
		{
			if (!IsDeleteAudit) return await base.DeleteAsync(predicate, cancellationToken);

			List<TEntity> items = await base.Select.Where(predicate).ToListAsync(cancellationToken);
			if (items.Count == 0)
			{
				return 0;
			}

			foreach (var entity in items)
			{
				BeforeDelete(entity);
			}

			return await base.UpdateAsync(items, cancellationToken);
		}

		#endregion

		#region InsertOrUpdate
		public override TEntity InsertOrUpdate(TEntity entity)
		{
			BeforeInsert(entity);
			return base.InsertOrUpdate(entity);
		}

		public override async Task<TEntity> InsertOrUpdateAsync(TEntity entity,
			CancellationToken cancellationToken = default)
		{
			BeforeInsert(entity);
			return await base.InsertOrUpdateAsync(entity, cancellationToken);
		}

		#endregion
	}
}
