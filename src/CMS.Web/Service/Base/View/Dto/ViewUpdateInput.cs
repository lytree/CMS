﻿using CMS.Web.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CMS.Web.Service.Base.View.Dto;

/// <summary>
/// 修改
/// </summary>
public class ViewUpdateInput : ViewAddInput
{
	/// <summary>
	/// 视图Id
	/// </summary>
	[Required]
	[ValidateRequired("请选择视图")]
	public long Id { get; set; }
}