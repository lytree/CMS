using System;
using LinCms.Data;

namespace CMS.Web.Service.Blog.Articles;

public class ArticleSearchDto : PageDto
{
	public long? ClassifyId { get; set; }
	public long? ChannelId { get; set; }
	public long? TagId { get; set; }
	public string Title { get; set; }
	public long? UserId { get; set; }

	public override string ToString()
	{
		return $"{ClassifyId}:{ChannelId}:{TagId}:{Title}:{UserId}:{Count}:{Page}:{Sort}";
	}

}