using CMS.Web.Attributes;
using System.ComponentModel.DataAnnotations;


namespace CMS.Web.Service.User.Permission.Dto;

public class PermissionUpdateGroupInput : PermissionAddGroupInput
{
	/// <summary>
	/// 权限Id
	/// </summary>
	[Required]
	[ValidateRequired("请选择分组")]
	public long Id { get; set; }
}