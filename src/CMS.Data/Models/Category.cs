using FreeSql.DataAnnotations;

namespace CMS.Data.Models;
[Table(Name = "category")]
public class Category : BaseEntity
{
	[Column(IsIdentity = true, IsPrimary = true)]
	public int Id { get; set; }

	[Column(Name = "category_name")]
	public string Name { get; set; }
	[Column(Name = "parent_id")]
	public int ParentId { get; set; }
	/// <summary>
	/// 分类是否可见
	/// </summary> 
	[Column(Name = "visible")]
	public bool Visible { get; set; } = true;
	/// <summary>
	/// 分类封面
	/// </summary>
	[Column(Name = "category_cover")]
	public string Cover { get; set; }
}