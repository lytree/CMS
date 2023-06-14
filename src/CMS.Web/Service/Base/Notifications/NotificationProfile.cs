using AutoMapper;
using CMS.Data.Model.Entities.Blog;

namespace CMS.Web.Service.Base.Notifications;

public class NotificationProfile : Profile
{
	public NotificationProfile()
	{
		CreateMap<NotificationEntity, NotificationDto>();
		CreateMap<ArticleEntity, ArticleEntry>();
		CreateMap<CommentEntity, CommentEntry>();
		CreateMap<CreateNotificationDto, NotificationEntity>();
	}
}