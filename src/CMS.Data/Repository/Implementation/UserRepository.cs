using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CMS.Data.Model.Entities.User;
using CMS.Data.Repository;
using FreeSql;

namespace CMS.Data.Repository.Implementation;

public class UserRepository : AuditBaseRepository<CMSUser, long>, IUserRepository
{
	public UserRepository(UnitOfWorkManager unitOfWorkManager) : base(unitOfWorkManager?.Orm, unitOfWorkManager)
	{
	}

	/// <summary>
	/// 根据条件得到用户信息
	/// </summary>
	/// <param name="expression"></param>
	/// <returns></returns>
	public Task<CMSUser> GetUserAsync(Expression<Func<CMSUser, bool>> expression)
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
		return UpdateDiy.Set(r => new CMSUser()
		{
			LastLoginTime = DateTime.Now
		}).Where(r => r.Id == userId).ExecuteAffrowsAsync();
	}
}