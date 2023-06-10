using System;
using CMS.Web.Service.Cms.Users;
using IGeekFan.FreeKit.Extras.AuditEntity;
using LinCms.Entities.Blog;

namespace CMS.Web.Service.Blog.Notifications;

public class NotificationDto : EntityDto<long>
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