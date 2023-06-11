using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.Model.Const;
/// <summary>
/// 统一ClaimTypes
/// </summary>
public static class ClaimTypes
{
	/// <summary>
	/// 租户
	/// </summary>
	public const string TenantName = "tenantname";
	/// <summary>
	/// 租户Id
	/// </summary>
	public const string TenantId = "tenantid";
	/// <summary>
	/// 用户Id
	/// </summary>
	public const string NameIdentifier = System.Security.Claims.ClaimTypes.NameIdentifier;
	/// <summary>
	/// 登录名
	/// </summary>
	public const string UserName = System.Security.Claims.ClaimTypes.Name;
	/// <summary>
	/// 邮件
	/// </summary>
	public const string Email = System.Security.Claims.ClaimTypes.Email;
	/// <summary>
	/// 角色
	/// </summary>
	public const string Role = System.Security.Claims.ClaimTypes.Role;
	/// <summary>
	/// 手机号
	/// </summary>
	public const string PhoneNumber = System.Security.Claims.ClaimTypes.MobilePhone;
	/// <summary>
	/// 姓名
	/// </summary>
	public const string Name = System.Security.Claims.ClaimTypes.GivenName;
	/// <summary>
	/// 姓名
	/// </summary>
	public const string Gender = System.Security.Claims.ClaimTypes.Gender;
}
