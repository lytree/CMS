﻿
using CMS.Data.Attributes;
using CMS.Data.Model.Core;
using FreeSql;
using FreeSql.DataAnnotations;


namespace CMS.Data.Model.Entities.User;

/// <summary>
/// 租户
/// </summary>
[Table(Name = "ad_tenant")]
public partial class TenantEntity : EntityBase
{
	/// <summary>
	/// 授权用户
	/// </summary>
	public long UserId { get; set; }

	/// <summary>
	/// 用户
	/// </summary>
	[NotGen]
	public UserEntity User { get; set; }


	/// <summary>
	/// 租户类型
	/// </summary>
	public TenantTypes? TenantType { get; set; } = TenantTypes.Tenant;

	/// <summary>
	/// 数据库注册键
	/// </summary>
	[Column(StringLength = 50)]
	public string DbKey { get; set; }

	/// <summary>
	/// 数据库
	/// </summary>
	[Column(MapType = typeof(int?))]
	public DataType? DbType { get; set; }

	/// <summary>
	/// 连接字符串
	/// </summary>
	[Column(StringLength = 500)]
	public string ConnectionString { get; set; }

	/// <summary>
	/// 启用
	/// </summary>
	public bool Enabled { get; set; } = true;

	/// <summary>
	/// 说明
	/// </summary>
	[Column(StringLength = 500)]
	public string Description { get; set; }

}