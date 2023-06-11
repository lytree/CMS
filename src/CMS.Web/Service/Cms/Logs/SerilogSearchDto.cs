﻿using CMS.Web.Data;
using System;


namespace CMS.Web.Service.Cms.Logs;

public class SerilogSearchDto : PageDto
{
	public string Keyword { get; set; }
	public int? LogLevel { get; set; }
	/// <summary>
	/// 起始时间
	/// </summary>
	public DateTime? Start { get; set; }
	/// <summary>
	/// 结束时间
	/// </summary>
	public DateTime? End { get; set; }
}