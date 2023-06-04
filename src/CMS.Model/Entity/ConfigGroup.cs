
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace CMS.Model.Entity;

/// <summary>
/// 配置项目
/// </summary>
[Table("config_group")]
public class ConfigGroup : BaseEntity
{
	[Column("id")]
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	[Column("group_name", TypeName = "varchar(200)")]
	public string Name { get; set; }

	[Column("group_key", TypeName = "varchar(200)")]
	public string Key { get; set; }
	[Column("group_status")]
	public int Status { get; set; }

	[Column("group_desc", TypeName = "varchar(1024)")]
	public string? Description { get; set; }

	public IEnumerable<ConfigItem> Items { get; } = new List<ConfigItem>();
}