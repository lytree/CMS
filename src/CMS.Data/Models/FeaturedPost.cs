using FreeSql.DataAnnotations;

namespace CMS.Data.Models;

/// <summary>
/// 推荐文章
/// </summary>
[Table(Name = "featured_post")]
public class FeaturedPost
{
	[Column(IsIdentity = true, IsPrimary = true)]
	public int Id { get; set; }
	[Column(Name = "post_id")]
	public string PostId { get; set; }

	[Column(Name = "post_desc")]
	public string Description { get; set; }
	public Post Post { get; set; }
}