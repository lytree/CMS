using CMS.Web.Attributes;
using System.ComponentModel.DataAnnotations;


namespace CMS.Web.Service.User.Role.Dto;

/// <summary>
/// 修改
/// </summary>
public partial class RoleUpdateInput : RoleAddInput
{
	/// <summary>
	/// 角色Id
	/// </summary>
	[Required]
	[ValidateRequired("请选择角色")]
	public long Id { get; set; }
}