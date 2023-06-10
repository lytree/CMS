using System;
using System.Threading.Tasks;
using CMS.Web.Service.Blog.ArticleDrafts;
using IGeekFan.FreeKit.Extras.FreeSql;
using LinCms.Entities.Blog;
using LinCms.Exceptions;
using LinCms.Security;

namespace CMS.Web.Service.Blog.Articles;

public class ArticleDraftService : ApplicationService, IArticleDraftService
{
	private readonly IAuditBaseRepository<ArticleDraft> _articleDraftRepository;
	public ArticleDraftService(IAuditBaseRepository<ArticleDraft> articleDraftRepository)
	{
		_articleDraftRepository = articleDraftRepository;
	}

	public async Task<ArticleDraftDto> GetAsync(long id)
	{
		ArticleDraft articleDraft = await _articleDraftRepository.Where(r => r.Id == id && r.CreateUserId == CurrentUser.FindUserId()).ToOneAsync();
		return Mapper.Map<ArticleDraftDto>(articleDraft);
	}

	public async Task UpdateAsync(long id, UpdateArticleDraftDto updateArticleDto)
	{
		ArticleDraft articleDraft = await _articleDraftRepository.Select.Where(r => r.Id == id).ToOneAsync();
		if (articleDraft != null && articleDraft.CreateUserId != CurrentUser.FindUserId())
		{
			throw new CMSException("您无权修改他人的随笔");
		}
		if (articleDraft == null)
		{
			articleDraft = new ArticleDraft { Id = id, CreateUserId = CurrentUser.FindUserId() ?? 0, CreateTime = DateTime.Now };
		}
		Mapper.Map(updateArticleDto, articleDraft);
		await _articleDraftRepository.InsertOrUpdateAsync(articleDraft);
	}
}