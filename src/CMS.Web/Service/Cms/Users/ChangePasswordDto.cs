using CMS.Web.Service.Cms.Admins;

namespace CMS.Web.Service.Cms.Users;

public class ChangePasswordDto : ResetPasswordDto
{

	// [Required(ErrorMessage = "原密码不可为空")]
	public string OldPassword { get; set; }
}