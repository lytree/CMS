﻿using System.ComponentModel.DataAnnotations;

namespace CMS.Web.Service.Cms.Account;

public class ResetEmailPasswordDto
{
	[Required(ErrorMessage = "非法请求")]
	public string Email { get; set; }
	[Required(ErrorMessage = "非法请求")]
	public string PasswordResetCode { get; set; }

	[Required(ErrorMessage = "请输入验证码")]
	public string ResetCode { get; set; }
	[Required(ErrorMessage = "请输入你的新密码")]
	[RegularExpression("^[A-Za-z0-9_*&$#@]{6,22}$", ErrorMessage = "密码长度必须在6~22位之间，包含字符、数字和 _")]
	public string Password { get; set; }
}