﻿using System;
using System.Collections.Generic;
using CMS.Web.Service.Cms.Groups;
using IGeekFan.FreeKit.Extras.AuditEntity;
using LinCms.Data.Enums;

namespace CMS.Web.Service.Cms.Users;

public class UserInformation : EntityDto<long>
{
	/// <summary>
	/// 昵称
	/// </summary>
	public string Nickname { get; set; }
	/// <summary>
	///  用户默认生成图像，为null、头像url
	/// </summary>
	public string Avatar { get; set; }
	/// <summary>
	/// 电子邮箱
	/// </summary>
	public string Email { get; set; }
	/// <summary>
	/// 是否为超级管理员 
	/// </summary>
	public bool Admin { get; set; } = false;
	/// <summary>
	/// 当前用户是否为激活状态，非激活状态默认失去用户权限 ; 1 -> 激活 | 2 -> 非激活
	/// </summary>
	public UserStatus Active { get; set; }
	/// <summary>
	/// 用户所属的权限组id
	/// </summary>
	public List<GroupDto> Groups { get; set; }

	public string Introduction { get; set; }
	public string BlogAddress { get; set; }
	public string Username { get; set; }
	public DateTime UpdateTime { get; set; }
	public DateTime CreateTime { get; set; }

	public List<IDictionary<string, object>> Permissions { get; set; }
}