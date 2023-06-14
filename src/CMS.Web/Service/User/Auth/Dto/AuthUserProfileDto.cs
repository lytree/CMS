﻿namespace CMS.Web.Service.User.Auth.Dto;

/// <summary>
/// 用户个人信息
/// </summary>
public class AuthUserProfileDto
{
	/// <summary>
	/// 账号
	/// </summary>
	public string UserName { get; set; }

	/// <summary>
	/// 姓名
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// 昵称
	/// </summary>
	public string NickName { get; set; }

	/// <summary>
	/// 头像
	/// </summary>
	public string Avatar { get; set; }
}