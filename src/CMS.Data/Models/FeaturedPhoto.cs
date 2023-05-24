using FreeSql.DataAnnotations;

namespace CMS.Data.Models;

/// <summary>
/// 推荐图片
/// </summary>
[Table(Name = "featured_photo")]
public class FeaturedPhoto
{
	[Column(IsIdentity = true, IsPrimary = true)]
	public int Id { get; set; }
	[Column(Name = "photo_id")]
	public string PhotoId { get; set; }
	[Column(Name = "photo_desc")]
	public string Description { get; set; }
	public Photo Photo { get; set; }
}