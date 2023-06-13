using CMS.Data.Model.Const;
using System;


namespace CMS.Data.Attributes;

/// <summary>
/// 启用权限库事务
/// </summary>
[AttributeUsage(AttributeTargets.Method, Inherited = true)]
public class AdminTransactionAttribute : TransactionAttribute
{
	public AdminTransactionAttribute() : base(DbKeys.AppDb)
	{
	}
}