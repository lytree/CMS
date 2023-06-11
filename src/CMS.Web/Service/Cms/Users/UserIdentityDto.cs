using System;


namespace CMS.Web.Service.Cms.Users;

public class UserIdentityDto
{
	public long Id { get; set; }
	public string IdentityType { get; set; }

	public string Identifier { get; set; }

	public string ExtraProperties { get; set; }

	public DateTime CreateTime { get; set; }
}