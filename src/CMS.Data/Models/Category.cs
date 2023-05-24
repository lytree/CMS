using FreeSql.DataAnnotations;

namespace CMS.Data.Models;
[Table(Name = "category")]
public class Category
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

	public Category? Parent { get; set; }
	public List<Post> Posts { get; set; }
}