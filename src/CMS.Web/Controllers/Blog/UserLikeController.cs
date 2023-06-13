﻿using System;
using System.Threading.Tasks;
using CMS.Data.Model.Entities.Blog;
using CMS.Data.Repository;
using CMS.Web.Data;
using CMS.Web.Extensions;
using CMS.Web.Service.Blog.Notifications;
using CMS.Web.Service.Blog.UserLikes;
using DotNetCore.CAP;
using FreeSql;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Controllers.Blog;

/// <summary>
///  用户点赞随笔
/// </summary>
[ApiExplorerSettings(GroupName = "blog")]
[Area("blog")]
[Route("api/blog/user-like")]
[ApiController]
[Authorize]
public class UserLikeController : ApiControllerBase
{
	private readonly IAuditBaseRepository<ArticleEntity, long> _articleRepository;
	private readonly IAuditBaseRepository<Comment, long> _commentRepository;
	private readonly ICapPublisher _capBus;
	private readonly IUserLikeService _userLikeService;
	private readonly UnitOfWorkManager _unitOfWorkManager;
	public UserLikeController(
		IAuditBaseRepository<ArticleEntity, long> articleRepository,
		IAuditBaseRepository<Comment, long> commentRepository,
		ICapPublisher capBus,
		UnitOfWorkManager unitOfWorkManager,
		IUserLikeService userLikeService
	) : base()
	{
		_articleRepository = articleRepository;
		_commentRepository = commentRepository;
		_capBus = capBus;
		_userLikeService = userLikeService;
		_unitOfWorkManager = unitOfWorkManager;
	}

	/// <summary>
	/// 用户点赞/取消点赞随笔、评论 
	/// </summary>
	/// <param name="createUpdateUserLike"></param>
	/// <returns></returns>
	[HttpPost]
	public async Task<UnifyResponseDto> CreateOrCancelAsync([FromBody] CreateUpdateUserLikeDto createUpdateUserLike)
	{
		IUnitOfWork unitOfWork = _unitOfWorkManager.Begin();
		using ICapTransaction capTransaction = unitOfWork.BeginTransaction(_capBus, false);

		bool isCancel = await _userLikeService.CreateOrCancelAsync(createUpdateUserLike);

		await PublishUserLikeNotification(createUpdateUserLike, isCancel);

		capTransaction.Commit(unitOfWork);

		return UnifyResponseDto.Success(isCancel == false ? "点赞成功" : "已取消点赞");
	}

	/// <summary>
	/// 根据用户点赞类型：随笔、评论，得到消息的NotificationRespUserId的值
	/// 
	/// </summary>
	/// <param name="createUpdateUserLike"></param>
	/// <param name="isCancel"></param>
	/// <returns></returns>
	private async Task PublishUserLikeNotification(CreateUpdateUserLikeDto createUpdateUserLike, bool isCancel)
	{
		var createNotificationDto = new CreateNotificationDto()
		{
			//todo 用户信息
			UserInfoId = _currentUser.FindUserId() ?? 0,
			CreateTime = DateTime.Now,
			IsCancel = isCancel
		};

		switch (createUpdateUserLike.SubjectType)
		{
			case UserLikeSubjectType.UserLikeArticle:

				ArticleEntity subjectArticle = await _articleRepository.Where(r => r.Id == createUpdateUserLike.SubjectId).ToOneAsync();

				createNotificationDto.NotificationRespUserId = subjectArticle.CreateUserId.Value;
				createNotificationDto.NotificationType = NotificationType.UserLikeArticle;
				createNotificationDto.ArticleId = createUpdateUserLike.SubjectId;
				break;

			case UserLikeSubjectType.UserLikeComment:

				Comment subjectComment = await _commentRepository.Where(r => r.Id == createUpdateUserLike.SubjectId).ToOneAsync();

				createNotificationDto.NotificationRespUserId = subjectComment.CreateUserId.Value;
				createNotificationDto.NotificationType = NotificationType.UserLikeArticleComment;
				createNotificationDto.ArticleId = subjectComment.SubjectId;
				createNotificationDto.CommentId = createUpdateUserLike.SubjectId;
				break;
		}

		if (createNotificationDto.NotificationRespUserId != 0 && _currentUser.FindUserId() != createNotificationDto.NotificationRespUserId)
		{
			await _capBus.PublishAsync(CreateNotificationDto.CreateOrCancelAsync, createNotificationDto);
		}
	}
}