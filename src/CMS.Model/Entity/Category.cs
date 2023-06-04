

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Model.Entity;
[Table("category")]
public class Category : BaseEntity
{
	[Column("id")]
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	[Column("category_name",TypeName = "varchar(200)")]
	public string Name { get; set; }

	[Column("parent_id")]
	public int ParentId { get; set; }
	/// <summary>
	/// 分类是否可见
	/// </summary> 
	[Column("visible")]
	public bool Visible { get; set; } = true;
	/// <summary>
	/// 分类封面
	/// </summary>
	[Column("category_cover",TypeName = "varchar(200)")]
	public string Cover { get; set; }

	public virtual IEnumerable<Post> Posts { get;  } = new List<Post>();
}