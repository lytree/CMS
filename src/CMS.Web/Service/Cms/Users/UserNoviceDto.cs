using System;
using IGeekFan.FreeKit.Extras.AuditEntity;

namespace CMS.Web.Service.Cms.Users;

public class UserNoviceDto : EntityDto<long>
{
	public string Introduction { get; set; }
	public string Username { get; set; }
	public string Nickname { get; set; }
	public string Avatar { get; set; }
	public DateTime CreateTime { get; set; }
	public DateTime LastLoginTime { get; set; }
}