﻿using CMS.Web.Attributes;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;


namespace CMS.Web.Service.User.User.Dto;

/// <summary>
/// 修改
/// </summary>
public partial class UserUpdateInput : UserFormInput
{
	/// <summary>
	/// 主键Id
	/// </summary>
	[Required]
	[ValidateRequired("请选择用户")]
	public override long Id { get; set; }
}