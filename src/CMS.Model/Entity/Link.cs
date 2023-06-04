

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Model.Entity;

/// <summary>
/// 友情链接
/// </summary>
/// 
[Table("link")]
public class Link : BaseEntity
{
	[Column("id")]
	[Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int? Id { get; set; }

	/// <summary>
	/// 网站名称
	/// </summary>
	[Column("link_name",TypeName = "varchar(200)")]
	public string Name { get; set; }

	/// <summary>
	/// 介绍
	/// </summary>
	[Column("link_desc",TypeName = "varchar(1024)")]
	public string? Description { get; set; }

	/// <summary>
	/// 网址
	/// </summary>
	[Column("link_url",TypeName = "varchar(200)")]
	public string Url { get; set; }

	/// <summary>
	/// 是否显示
	/// </summary>
	[Column("link_visible")]
	public bool Visible { get; set; }
}