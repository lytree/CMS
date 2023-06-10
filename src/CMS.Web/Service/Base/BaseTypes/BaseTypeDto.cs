using System;

namespace CMS.Web.Service.Base.BaseTypes;

public class BaseTypeDto
{
	public string TypeCode { get; set; }
	public string FullName { get; set; }
	public int? SortCode { get; set; }
	public DateTime CreateTime { get; set; }
}