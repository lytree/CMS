﻿using System.ComponentModel.DataAnnotations;
using ZhonTai.Admin.Core.Validators;

namespace CMS.Web.Service.User.Permission.Dto;

public class PermissionUpdateDotInput : PermissionAddDotInput
{
	/// <summary>
	/// 权限Id
	/// </summary>
	[Required]
	[ValidateRequired("请选择权限点")]
	public long Id { get; set; }
}