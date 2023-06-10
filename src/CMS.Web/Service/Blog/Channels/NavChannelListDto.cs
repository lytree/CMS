using System;
using System.Collections.Generic;
using CMS.Data.Model.Entities.Base;
using CMS.Web.Service.Blog.Tags;

namespace CMS.Web.Service.Blog.Channels;

public class NavChannelListDto : Entity
{
	/// <summary>
	/// 技术频道名称
	/// </summary>

	public string ChannelName { get; set; }

	/// <summary>
	/// 编码
	/// </summary>
	public string ChannelCode { get; set; }


	public List<TagDto> Tags { get; set; }
}