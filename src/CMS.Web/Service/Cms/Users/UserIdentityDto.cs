using System;
using IGeekFan.FreeKit.Extras.AuditEntity;

namespace CMS.Web.Service.Cms.Users;

public class UserIdentityDto : EntityDto<long>
{
	public string IdentityType { get; set; }

	public string Identifier { get; set; }

	public string ExtraProperties { get; set; }

	public DateTime CreateTime { get; set; }
}