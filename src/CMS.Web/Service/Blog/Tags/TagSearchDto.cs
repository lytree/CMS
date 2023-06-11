using CMS.Web.Data;
using System;
using System.Collections.Generic;

namespace CMS.Web.Service.Blog.Tags;

public class TagSearchDto : PageDto
{
	public List<long> TagIds { get; set; }
	public string TagName { get; set; }

	public bool? Status { get; set; }
}