﻿using System;
using IGeekFan.FreeKit.Extras.AuditEntity;

namespace CMS.Web.Service.Cms.Settings;

public class SettingDto : EntityDto<long>
{
	/// <summary>
	/// 键
	/// </summary>
	public string Name { get; set; }
	/// <summary>
	/// 值
	/// </summary>
	public string Value { get; set; }
	/// <summary>
	/// 提供者键
	/// </summary>
	public string ProviderName { get; set; }
	/// <summary>
	/// 提供者值
	/// </summary>
	public string ProviderKey { get; set; }
}