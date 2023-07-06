﻿using System.ComponentModel.DataAnnotations;

namespace CMS.Web.Service.User.Auth.Dto;

/// <summary>
/// 登录信息
/// </summary>
public class AuthLoginInput
{
	/// <summary>
	/// 账号
	/// </summary>
	[Required(ErrorMessage = "用户名不能为空")]
	public string UserName { get; set; }

	/// <summary>
	/// 密码
	/// </summary>
	[Required(ErrorMessage = "密码不能为空")]
	public string Password { get; set; }

	/// <summary>
	/// 密码键
	/// </summary>
	public string? PasswordKey { get; set; }

	/// <summary>
	/// 验证码Id
	/// </summary>
	public string CaptchaId { get; set; }

	/// <summary>
	/// 验证码数据
	/// </summary>
	public string CaptchaData { get; set; }
}