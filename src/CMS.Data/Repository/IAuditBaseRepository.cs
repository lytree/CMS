using FreeSql;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.Repository
{
	public interface IAuditBaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
		where TEntity : class
		where TKey : IEquatable<TKey>
	{
		TEntity Get(TKey id);
		int Delete(TKey id);
		Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken = default);
		Task<int> DeleteAsync(TKey id, CancellationToken cancellationToken = default);
	}
}
