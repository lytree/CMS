using System;
using IGeekFan.FreeKit.Extras.AuditEntity;

namespace CMS.Web.Service.Blog.Notifications;

public class ArticleEntry : EntityDto<long>
{
	public string Title { get; set; }

}