

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Model.Entity;

/// <summary>
/// 置顶文章
/// </summary>
[Table("top_post")]
public class TopPost
{
	[Column("id")]
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	public int PostId { get; set; }

	public virtual Post Post { get; set; }
}