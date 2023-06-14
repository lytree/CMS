using CMS.Data.Model.Entities.Blog;
using System;


namespace CMS.Web.Service.Base.Notifications;

public class CreateNotificationDto
{
	public const string CreateOrCancelAsync = "CreateNotificationDto.CreateOrCancelAsync";
	public NotificationType NotificationType { get; set; }
	public long? ArticleId { get; set; }
	public long? CommentId { get; set; }
	public long NotificationRespUserId { get; set; }
	public long UserInfoId { get; set; }

	public bool IsCancel { get; set; }
	public DateTime CreateTime { get; set; }
}