using System;


namespace CMS.Web.Service.Blog.Tags;

public class TagDto
{
	public TagDto(long id, string tagName)
	{
		Id = id;
		TagName = tagName;
	}
	public long Id { get; set; }
	public string TagName { get; set; }
}