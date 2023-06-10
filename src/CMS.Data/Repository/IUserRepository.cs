﻿using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CMS.Data.Model.Entities.User;
using CMS.Data.Repository;
using FreeSql;

namespace CMS.Data.Repository
{
	public interface IUserRepository : IAuditBaseRepository<LinUser, long>
	{
		/// <summary>
		/// 根据条件得到用户信息
		/// </summary>
		/// <param name="expression"></param>
		/// <returns></returns>
		Task<LinUser> GetUserAsync(Expression<Func<LinUser, bool>> expression);

		/// <summary>
		/// 根据用户Id更新用户的最后登录时间
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		Task UpdateLastLoginTimeAsync(long userId);
	}
}