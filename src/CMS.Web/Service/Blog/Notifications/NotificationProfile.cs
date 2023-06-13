using AutoMapper;
using CMS.Data.Model.Entities.Blog;

namespace CMS.Web.Service.Blog.Notifications;

public class NotificationProfile : Profile
{
	public NotificationProfile()
	{
		CreateMap<Notification, NotificationDto>();
		CreateMap<ArticleEntity, ArticleEntry>();
		CreateMap<Comment, CommentEntry>();
		CreateMap<CreateNotificationDto, Notification>();
	}
}