﻿using System;
using System.Threading.Tasks;
using CMS.Web.Service.Blog.ArticleDrafts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Controllers.Blog;

/// <summary>
/// 随笔草稿箱，自动保存随笔
/// </summary>
[Area("blog")]
[ApiExplorerSettings(GroupName = "blog")]
[Route("api/blog/articles/draft")]
[ApiController]
[Authorize]
public class ArticleDraftController : ControllerBase
{
	private readonly IArticleDraftService _articleDraftService;
	public ArticleDraftController(IArticleDraftService articleDraftService)
	{
		_articleDraftService = articleDraftService;
	}

	[HttpPut("{id}")]
	public Task UpdateAsync(long id, [FromBody] UpdateArticleDraftDto updateArticleDto)
	{
		return _articleDraftService.UpdateAsync(id, updateArticleDto);
	}

	/// <summary>
	/// 用户的随笔草稿详情
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	[HttpGet("{id}")]
	public Task<ArticleDraftDto> GetAsync(long id)
	{
		return _articleDraftService.GetAsync(id);
	}
}