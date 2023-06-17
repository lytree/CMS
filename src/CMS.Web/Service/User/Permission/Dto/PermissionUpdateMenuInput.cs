using CMS.Web.Attributes;
using System.ComponentModel.DataAnnotations;


namespace CMS.Web.Service.User.Permission.Dto;

public class PermissionUpdateMenuInput : PermissionAddMenuInput
{
	/// <summary>
	/// 权限Id
	/// </summary>
	[Required]
	[ValidateRequired("请选择菜单")]
	public long Id { get; set; }
}