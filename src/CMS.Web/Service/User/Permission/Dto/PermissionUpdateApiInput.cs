using System.ComponentModel.DataAnnotations;
using ZhonTai.Admin.Core.Validators;

namespace CMS.Web.Service.User.Permission.Dto;

public class PermissionUpdateApiInput : PermissionAddApiInput
{
	/// <summary>
	/// 权限Id
	/// </summary>
	[Required]
	[ValidateRequired("请选择接口")]
	public long Id { get; set; }
}