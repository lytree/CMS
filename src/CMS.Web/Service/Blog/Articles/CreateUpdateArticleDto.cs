﻿using CMS.Data.Model.Entities.Blog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace CMS.Web.Service.Blog.Articles;

public class CreateUpdateArticleDto
{
	public long? ClassifyId { get; set; }
	public long? ChannelId { get; set; }
	[MaxLength(200)]
	public string Title { get; set; }
	[MaxLength(400)]
	public string Keywords { get; set; }
	[MaxLength(400)]
	public string Source { get; set; }
	[MaxLength(400)]
	public string Excerpt { get; set; }
	[Required(ErrorMessage = "随笔内容不能为空")]
	public string Content { get; set; }
	[MaxLength(400)]
	public string Thumbnail { get; set; }
	public bool IsAudit { get; set; }
	public bool Recommend { get; set; }
	public bool IsStickie { get; set; }
	[MaxLength(50)]
	public string Archive { get; set; }

	public ArticleType ArticleType { get; set; }

	public int Editor { get; set; } = 1;

	public List<long> TagIds { get; set; }
}