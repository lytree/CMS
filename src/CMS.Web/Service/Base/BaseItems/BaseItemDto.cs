using System;

namespace CMS.Web.Service.Base.BaseItems;

public class BaseItemDto 
{
	public long BaseTypeId { get; set; }
	public string ItemCode { get; set; }
	public string ItemName { get; set; }
	public bool Status { get; set; }
	public int? SortCode { get; set; }
	public DateTime CreateTime { get; set; }
}