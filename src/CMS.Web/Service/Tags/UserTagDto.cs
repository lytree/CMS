using System;

namespace CMS.Web.Service.Blog.Tags;

public class UserTagDto
{
	public long TagId { get; set; }
	public long CreateUserId { get; set; }
	public bool IsSubscribeed { get; set; }
}