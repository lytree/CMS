using System.ComponentModel.DataAnnotations;

namespace CMS.Web.Service.User.User.Dto;

/// <summary>
/// 用户表单
/// </summary>
public class UserFormInput
{
	/// <summary>
	/// 用户Id
	/// </summary>
	public virtual long Id { get; set; }

	/// <summary>
	/// 账号
	/// </summary>
	[Required(ErrorMessage = "请输入账号")]
	public string UserName { get; set; }

	/// <summary>
	/// 姓名
	/// </summary>
	[Required(ErrorMessage = "请输入姓名")]
	public string Name { get; set; }

	/// <summary>
	/// 手机号
	/// </summary>
	public string Mobile { get; set; }

	/// <summary>
	/// 邮箱
	/// </summary>
	public string Email { get; set; }

	/// <summary>
	/// 角色Ids
	/// </summary>
	public virtual long[] RoleIds { get; set; }
}