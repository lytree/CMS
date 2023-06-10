using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CMS.Data.Model.Entities.User;
using CMS.Data.Repository;
using FreeSql;

namespace CMS.Data.Repository.Implementation;

public class UserRepository : AuditBaseRepository<LinUser, long>, IUserRepository
{
	public UserRepository(UnitOfWorkManager unitOfWorkManager) : base(unitOfWorkManager?.Orm, unitOfWorkManager)
	{
	}

	/// <summary>
	/// 根据条件得到用户信息
	/// </summary>
	/// <param name="expression"></param>
	/// <returns></returns>
	public Task<LinUser> GetUserAsync(Expression<Func<LinUser, bool>> expression)
	{
		return Select.Where(expression).IncludeMany(r => r.LinGroups).ToOneAsync();
	}

	/// <summary>
	/// 根据用户Id更新用户的最后登录时间
	/// </summary>
	/// <param name="userId"></param>
	/// <returns></returns>
	public Task UpdateLastLoginTimeAsync(long userId)
	{
		return UpdateDiy.Set(r => new LinUser()
		{
			LastLoginTime = DateTime.Now
		}).Where(r => r.Id == userId).ExecuteAffrowsAsync();
	}
}