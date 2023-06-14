using System;


namespace CMS.Web.Service.Blog.Notifications;

public class CommentEntry 
{
	public long Id { get; set; }
	public long? RespId { get; set; }
	public string Text { get; set; }
}