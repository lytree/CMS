using System;
using IGeekFan.FreeKit.Extras.AuditEntity;

namespace CMS.Web.Service.Blog.Tags;

public class TagDto : EntityDto<long>
{
	public TagDto(long id, string tagName)
	{
		Id = id;
		TagName = tagName;
	}

	public string TagName { get; set; }
}