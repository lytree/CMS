using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Data.Exceptions;
using CMS.Data.Extensions;
using CMS.Data.Model.Entities;
using CMS.Data.Model.Entities.Blog;
using CMS.Data.Model.Enums;
using CMS.Data.Repository;
using CMS.Web.Data;
using CMS.Web.Service;
using CMS.Web.Service.Blog.Classifies;
using CMS.Web.Service.Blog.UserSubscribes;
using FreeSql;

namespace CMS.Web.Service.Blog.Articles;

public class ArticleService : ApplicationService, IArticleService
{
	private readonly IAuditBaseRepository<Article, long> _articleRepository;
	private readonly IAuditBaseRepository<ArticleDraft, long> _articleDraftRepository;
	private readonly IAuditBaseRepository<UserLike, long> _userLikeRepository;
	private readonly IAuditBaseRepository<Comment, long> _commentRepository;
	private readonly IAuditBaseRepository<TagArticle, long> _tagArticleRepository;
	private readonly IClassifyService _classifyService;
	private readonly IAuditBaseRepository<Tag, long> _tagRepository;
	private readonly IUserSubscribeService _userSubscribeService;
	private readonly IFileRepository _fileRepository;
	public ArticleService(
		IAuditBaseRepository<Article, long> articleRepository,
		IAuditBaseRepository<TagArticle, long> tagArticleRepository,
		IAuditBaseRepository<UserLike, long> userLikeRepository,
		IAuditBaseRepository<Comment, long> commentRepository,
		IClassifyService classifyService,
		IAuditBaseRepository<Tag, long> tagRepository,
		IUserSubscribeService userSubscribeService,
		IAuditBaseRepository<ArticleDraft, long> articleDraftRepository,
		IFileRepository fileRepository
	)
	{
		_articleRepository = articleRepository;
		_tagArticleRepository = tagArticleRepository;
		_userLikeRepository = userLikeRepository;
		_commentRepository = commentRepository;

		_classifyService = classifyService;
		_tagRepository = tagRepository;
		_userSubscribeService = userSubscribeService;
		_articleDraftRepository = articleDraftRepository;
		_fileRepository = fileRepository;
	}

	//[Cacheable]
	public async Task<PagedResultDto<ArticleListDto>> GetArticleAsync(ArticleSearchDto searchDto)
	{
		DateTime monthDays = DateTime.Now.AddDays(-30);
		DateTime weeklyDays = DateTime.Now.AddDays(-7);
		DateTime threeDays = DateTime.Now.AddDays(-3);

		List<Article> articles = await _articleRepository
			.Select
			.IncludeMany(r => r.Tags, r => r.Where(u => u.Status == true))
			//.IncludeMany(r => r.UserLikes, r => r.Where(u => u.CreateUserId == userId))
			.Where(r => r.IsAudit)
			.WhereCascade(r => r.IsDeleted == false)
			//.WhereIf(searchDto.UserId != null, r => r.CreateUserId == searchDto.UserId)
			.WhereIf(searchDto.TagId.HasValue, r => r.Tags.AsSelect().Any(u => u.Id == searchDto.TagId))
			.WhereIf(searchDto.ClassifyId.HasValue, r => r.ClassifyId == searchDto.ClassifyId)
			.WhereIf(searchDto.ChannelId.HasValue, r => r.ChannelId == searchDto.ChannelId)
			.WhereIf(searchDto.Title.IsNotNullOrEmpty(), r => r.Title.Contains(searchDto.Title))
			.WhereIf(searchDto.Sort == "THREE_DAYS_HOTTEST", r => r.CreateTime > threeDays)
			.WhereIf(searchDto.Sort == "WEEKLY_HOTTEST", r => r.CreateTime > weeklyDays)
			.WhereIf(searchDto.Sort == "MONTHLY_HOTTEST", r => r.CreateTime > monthDays)
			.OrderByDescending(
				searchDto.Sort == "THREE_DAYS_HOTTEST" || searchDto.Sort == "WEEKLY_HOTTEST" ||
				searchDto.Sort == "MONTHLY_HOTTEST" ||
				searchDto.Sort == "HOTTEST",
				r => r.ViewHits + r.LikesQuantity * 20 + r.CommentQuantity * 30)
			.OrderByDescending(r => r.CreateTime).ToPagerListAsync(searchDto, out long totalCount);

		List<ArticleListDto> articleDtos = articles
			.Select(a =>
			{
				ArticleListDto articleDto = Mapper.Map<ArticleListDto>(a);

				//articleDto.IsLiked = userId != null && a.UserLikes.Any();
				articleDto.ThumbnailDisplay = _fileRepository.GetFileUrl(articleDto.Thumbnail);

				return articleDto;
			})
			.ToList();

		return new PagedResultDto<ArticleListDto>(articleDtos, totalCount);
	}

	public async Task DeleteAsync(long id)
	{
		Article article = _articleRepository.Select.Where(r => r.Id == id).IncludeMany(r => r.Tags).ToOne();
		if (article.IsNotNull())
		{
			await _classifyService.UpdateArticleCountAsync(article.ClassifyId, 1);
			article.Tags?
				.ForEach(async (u) => { await UpdateArticleCountAsync(u.Id, -1); });
		}

		await _articleRepository.DeleteAsync(new Article { Id = id });
		await _tagArticleRepository.DeleteAsync(r => r.ArticleId == id);
		await _commentRepository.DeleteAsync(r => r.SubjectId == id);
		await _userLikeRepository.DeleteAsync(r => r.SubjectId == id);
	}

	public async Task<ArticleDto> GetAsync(long id)
	{
		Article article = await _articleRepository.Select
			.Include(r => r.Classify).IncludeMany(r => r.Tags).WhereCascade(r => r.IsDeleted == false).Where(a => a.Id == id).ToOneAsync();

		if (article.IsNull())
		{
			throw new CMSException("该随笔不存在");
		}

		ArticleDto articleDto = Mapper.Map<ArticleDto>(article);

		if (articleDto.Tags.IsNotNull())
		{
			articleDto.Tags.ForEach(r => { r.ThumbnailDisplay = _fileRepository.GetFileUrl(r.Thumbnail); });
		}

		if (articleDto.UserInfo.IsNotNull())
		{
			articleDto.UserInfo.Avatar = _fileRepository.GetFileUrl(articleDto.UserInfo.Avatar);
		}

		articleDto.IsLiked =
			await _userLikeRepository.Select.AnyAsync(r => r.SubjectId == id);
		articleDto.IsComment =
			await _commentRepository.Select.AnyAsync(
				r => r.SubjectId == id);
		articleDto.ThumbnailDisplay = _fileRepository.GetFileUrl(article.Thumbnail);

		return articleDto;
	}

	public async Task<long> CreateAsync(CreateUpdateArticleDto createArticle)
	{
		Article article = Mapper.Map<Article>(createArticle);
		article.Archive = DateTime.Now.ToString("yyy年MM月");
		article.WordNumber = createArticle.Content.Length;
		article.ReadingTime = createArticle.Content.Length / 800;

		article.Tags = new List<Tag>();
		foreach (var articleTagId in createArticle.TagIds)
		{
			article.Tags.Add(new Tag()
			{
				Id = articleTagId,
			});
			await UpdateArticleCountAsync(articleTagId, 1);
		}
		await _articleRepository.InsertAsync(article);

		await _articleDraftRepository.InsertAsync(new ArticleDraft(article.Id, createArticle.Content, createArticle.Title, createArticle.Editor));

		if (createArticle.ClassifyId != null)
		{
			await _classifyService.UpdateArticleCountAsync(createArticle.ClassifyId, 1);
		}
		return article.Id;
	}

	public async Task UpdateAsync(long id, CreateUpdateArticleDto updateArticleDto)
	{
		using var unitOfWork = UnitOfWorkManager.Begin(Propagation.Required, System.Data.IsolationLevel.ReadCommitted);
		try
		{
			Article article = _articleRepository.Select.Where(r => r.Id == id).ToOne();


			//if (article.CreateUserId != CurrentUser.FindUserId())
			//{
			//	throw new CMSException("您无权修改他人的随笔");
			//}

			if (article == null)
			{
				throw new CMSException("没有找到相关随笔");
			}

			if (article.ClassifyId != updateArticleDto.ClassifyId)
			{
				await _classifyService.UpdateArticleCountAsync(article.ClassifyId, -1);
				await _classifyService.UpdateArticleCountAsync(updateArticleDto.ClassifyId, 1);
			}

			Mapper.Map(updateArticleDto, article);
			article.WordNumber = article.Content.Length;
			article.ReadingTime = article.Content.Length / 800;
			await _articleRepository.UpdateAsync(article);

			ArticleDraft articleDraft = Mapper.Map<ArticleDraft>(article);
			bool exist = await _articleDraftRepository.Select.AnyAsync(r => r.Id == article.Id);
			if (exist)
			{
				await _articleDraftRepository.UpdateAsync(articleDraft);
			}
			else
			{
				await _articleDraftRepository.InsertAsync(articleDraft);
			}
			await UpdateTagAsync(id, updateArticleDto);
		}
		catch (Exception ex)
		{
			unitOfWork.Rollback();
		}
		unitOfWork.Commit();
	}

	/// <summary>
	///  随笔选择多个标签
	/// </summary>
	/// <param name="id"></param>
	/// <param name="updateArticleDto"></param>
	/// <returns></returns>
	private async Task UpdateTagAsync(long id, CreateUpdateArticleDto updateArticleDto)
	{
		List<long> tagIds = await _tagArticleRepository.Select.Where(r => r.ArticleId == id).ToListAsync(r => r.TagId);

		foreach (var tagId in tagIds)
		{
			await UpdateArticleCountAsync(tagId, -1);
		}

		_tagArticleRepository.Delete(r => r.ArticleId == id);

		List<TagArticle> tagArticles = new();

		foreach (var tagId in updateArticleDto.TagIds)
		{
			tagArticles.Add(new TagArticle()
			{
				ArticleId = id,
				TagId = tagId
			});
			await UpdateArticleCountAsync(tagId, 1);
		}
		await _tagArticleRepository.InsertAsync(tagArticles);
	}

	private async Task UpdateArticleCountAsync(long? id, int inCreaseCount)
	{
		if (id == null)
		{
			return;
		}
		Tag tag = await _tagRepository.Select.Where(r => r.Id == id).ToOneAsync();
		if (tag == null) { return; }
		//防止数量一直减，减到小于0
		if (inCreaseCount < 0)
		{
			if (tag.ArticleCount < -inCreaseCount)
			{
				return;
			}
		}
		tag.ArticleCount = tag.ArticleCount + inCreaseCount;

		await _tagRepository.UpdateAsync(tag);
	}

	//public async Task<PagedResultDto<ArticleListDto>> GetSubscribeArticleAsync(PageDto pageDto)
	//{
	//	List<long> subscribeUserIds = await _userSubscribeService.GetSubscribeUserIdAsync(userId);

	//	var articles = await _articleRepository
	//		.Select
	//		.Include(r => r.Classify)
	//		//.Include(r => r.UserInfo)
	//		.IncludeMany(r => r.Tags, r => r.Where(u => u.Status))
	//		//.IncludeMany(r => r.UserLikes)//, r => r.Where(u => u.CreateUserId == userId))
	//		.Where(r => r.IsAudit)
	//		//.WhereIf(subscribeUserIds.Count > 0, r => subscribeUserIds.Contains(r.CreateUserId.Value))
	//		.WhereIf(subscribeUserIds.Count == 0, r => false)
	//		.OrderByDescending(r => r.CreateTime).ToPagerListAsync(pageDto, out long totalCount);

	//	List<ArticleListDto> articleDtos = articles
	//		.Select(r =>
	//		{
	//			ArticleListDto articleDto = Mapper.Map<ArticleListDto>(r);
	//			//articleDto.IsLiked = r.UserLikes.Any();
	//			articleDto.ThumbnailDisplay = _fileRepository.GetFileUrl(articleDto.Thumbnail);
	//			return articleDto;
	//		})
	//		.ToList();

	//	return new PagedResultDto<ArticleListDto>(articleDtos, totalCount);
	//}

	public async Task UpdateLikeQuantityAysnc(long subjectId, int likesQuantity)
	{
		Article article = await _articleRepository.Where(r => r.Id == subjectId).ToOneAsync();
		article.UpdateLikeQuantity(likesQuantity);
		await _articleRepository.UpdateAsync(article);
	}

	public async Task UpdateCommentable(long id, bool commentable)
	{
		Article article = await _articleRepository.Select.Where(a => a.Id == id).ToOneAsync();
		if (article == null)
		{
			throw new CMSException("没有找到相关随笔", ErrorCode.NotFound);
		}
		//if (article.CreateUserId != CurrentUser.FindUserId())
		//{
		//	throw new CMSException("不是自己的随笔", ErrorCode.NoPermission);
		//}
		article.Commentable = commentable;
		await _articleRepository.UpdateAsync(article);
	}
}