using System;
using IGeekFan.FreeKit.Extras.AuditEntity;

namespace CMS.Web.Service.Blog.Notifications;

public class CommentEntry : EntityDto<long>
{
	public long? RespId { get; set; }
	public string Text { get; set; }
}