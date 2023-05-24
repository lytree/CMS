using FreeSql.DataAnnotations;

namespace CMS.Data.Models;

/// <summary>
/// 友情链接
/// </summary>
/// 
[Table(Name = "link")]
public class Link
{
	[Column(IsIdentity = true, IsPrimary = true)]
	public int Id { get; set; }

	/// <summary>
	/// 网站名称
	/// </summary>
	[Column(Name = "link_name")]
	public string Name { get; set; }

	/// <summary>
	/// 介绍
	/// </summary>
	[Column(Name = "link_desc")]
	public string? Description { get; set; }

	/// <summary>
	/// 网址
	/// </summary>
	[Column(Name = "link_url")]
	public string Url { get; set; }

	/// <summary>
	/// 是否显示
	/// </summary>
	[Column(Name = "link_visible")]
	public bool Visible { get; set; }
}