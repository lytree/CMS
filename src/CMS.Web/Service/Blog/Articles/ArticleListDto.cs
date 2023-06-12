using System;
using System.Collections.Generic;
using CMS.Data.Model.Entities.Base;
using CMS.Data.Model.Entities.Blog;
using CMS.Data.Utils;
using CMS.Web.Service.Blog.Tags;
using CMS.Web.Service.Cms.Users;

namespace CMS.Web.Service.Blog.Articles;

public class ArticleListDto
{
	public long Id { get; set; }
	/// <summary>
	/// 技术频道Id
	/// </summary>
	public long? ChannelId { get; set; }
	/// <summary>
	/// 几小时/秒前
	/// </summary>
	public string TimeSpan => CMSUtils.GetTimeDifferNow(CreateTime.ToDateTime());

	private readonly DateTime _now = DateTime.Now;
	public bool IsNew => DateTime.Compare(_now.AddDays(-2), CreateTime.ToDateTime()) <= 0;

	public string Title { get; set; }
	public string Keywords { get; set; }
	public string Excerpt { get; set; }
	public int ViewHits { get; set; }
	public int CommentQuantity { get; set; }
	public int LikesQuantity { get; set; }
	public string Thumbnail { get; set; }
	public string ThumbnailDisplay { get; set; }
	public bool IsAudit { get; set; }
	public bool Recommend { get; set; }
	public bool IsStickie { get; set; }
	public string Archive { get; set; }
	public ArticleType ArticleType { get; set; }
	public long? CreateUserId { get; set; }
	public string CreateUserName { get; set; }

	public DateTime CreateTime { get; set; }
	public string Author { get; set; }
	public bool IsLiked { get; set; }

	public OpenUserDto UserInfo { get; set; }

	public List<TagDto> Tags { get; set; }
}