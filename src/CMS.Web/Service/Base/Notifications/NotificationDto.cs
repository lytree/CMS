using System;
using CMS.Data.Model.Entities.Blog;
using CMS.Web.Service.Cms.Users;


namespace CMS.Web.Service.Base.Notifications;

public class NotificationDto
{
	public NotificationType NotificationType { get; set; }
	public OpenUserDto UserInfo { get; set; }
	public bool IsRead { get; set; }
	public long UserInfoId { get; set; }
	public long MessageRespUserId { get; set; }
	public DateTime CreateTime { get; set; }
	public ArticleEntry ArticleEntry { get; set; }
	public CommentEntry CommentEntry { get; set; }
}