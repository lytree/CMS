using FreeSql.DataAnnotations;
using System.Xml.Linq;

namespace CMS.Data.Models;

/// <summary>
/// 配置项目
/// </summary>
[Table(Name = "config_group")]
public class ConfigGroup
{
	[Column(IsIdentity = true, IsPrimary = true)]
	public int Id { get; set; }

	[Column(Name = "group_name")]
	public string Name { get; set; }

	[Column(Name = "group_key")]
	public string Key { get; set; }
	[Column(Name = "group_status")]
	public int Status { get; set; }

	[Column(Name = "group_desc")]
	public string? Description { get; set; }
}