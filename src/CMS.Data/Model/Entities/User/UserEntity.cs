using CMS.Data.Attributes;
using CMS.Data.Model.Core;
using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;

namespace CMS.Data.Model.Entities.User;

/// <summary>
/// 用户
/// </summary>
[Table(Name = "ad_user")]
[Index("idx_{tablename}_01", nameof(UserName) + "," + nameof(TenantId), true)]
public partial class UserEntity : EntityTenant
{
	[NotGen]
	public TenantEntity Tenant { get; set; }

	/// <summary>
	/// 账号
	/// </summary>
	[Column(StringLength = 60)]
	public string UserName { get; set; }

	/// <summary>
	/// 密码
	/// </summary>
	[Column(StringLength = 200)]
	public string Password { get; set; }

	/// <summary>
	/// 密码加密类型
	/// </summary>
	[Column(MapType = typeof(int?))]
	public PasswordEncryptType? PasswordEncryptType { get; set; }

	/// <summary>
	/// 姓名
	/// </summary>
	[Column(StringLength = 60)]
	public string Name { get; set; }

	/// <summary>
	/// 手机号
	/// </summary>
	[Column(StringLength = 20)]
	public string Mobile { get; set; }

	/// <summary>
	/// 邮箱
	/// </summary>
	[Column(StringLength = 100)]
	public string Email { get; set; }

	/// <summary>
	/// 昵称
	/// </summary>
	[Column(StringLength = 60)]
	public string NickName { get; set; }

	/// <summary>
	/// 头像
	/// </summary>
	[Column(StringLength = 500)]
	public string Avatar { get; set; }

	/// <summary>
	/// 用户状态
	/// </summary>
	[Column(MapType = typeof(int?))]
	public UserStatus? Status { get; set; }

	/// <summary>
	/// 用户类型
	/// </summary>
	[Column(MapType = typeof(int))]
	public UserType Type { get; set; } = UserType.DefaultUser;

	/// <summary>
	/// 启用
	/// </summary>
	public bool Enabled { get; set; } = true;

	/// <summary>
	/// 角色列表
	/// </summary>
	[NotGen]
	[Navigate(ManyToMany = typeof(UserRoleEntity))]
	public ICollection<RoleEntity> Roles { get; set; }

}