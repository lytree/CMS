﻿using System;


namespace CMS.Web.Service.Blog.Tags;

/// <summary>
/// 后台列表List
/// </summary>
public class TagListDto 
{
	public long Id { get; set; }
	public string Thumbnail { get; set; }
	public string ThumbnailDisplay { get; set; }
	public string TagName { get; set; }
	public long CreateUserId { get; set; }
	public DateTime? CreateTime { get; set; }
	public string Alias { get; set; }
	public int ArticleCount { get; set; }
	public int SubscribersCount { get; set; }
	public int ViewHits { get; set; }
	public bool Status { get; set; }
	public bool IsSubscribe { get; set; }
	public string Remark { get; set; }

}