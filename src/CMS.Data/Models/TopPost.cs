using FreeSql.DataAnnotations;

namespace CMS.Data.Models;

/// <summary>
/// 置顶文章
/// </summary>
[Table(Name = "top_post")]
public class TopPost
{
	[Column(IsIdentity = true, IsPrimary = true)]
	public int Id { get; set; }

	public int PostId { get; set; }

	public virtual Post Post { get; set; }
}