﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CMS.Data.Exceptions;
using CMS.Data.Model.Entities.Blog;
using CMS.Data.Model.Entities.User;
using CMS.Data.Repository;
using CMS.Web.Data;
using CMS.Web.Service.Blog.Notifications;
using DotNetCore.CAP;


namespace CMS.Web.Service.Blog.UserSubscribes;

public class UserSubscribeService : ApplicationService, IUserSubscribeService
{
	private readonly IUserRepository _userRepository;
	private readonly IAuditBaseRepository<UserSubscribe, long> _userSubscribeRepository;
	private readonly ICapPublisher _capBus;
	private readonly IFileRepository _fileRepository;
	public UserSubscribeService(IAuditBaseRepository<UserSubscribe, long> userSubscribeRepository, IUserRepository userRepository, ICapPublisher capBus, IFileRepository fileRepository)
	{
		_userSubscribeRepository = userSubscribeRepository;
		_userRepository = userRepository;
		_capBus = capBus;
		_fileRepository = fileRepository;
	}

	public async Task<List<long>> GetSubscribeUserIdAsync(long userId)
	{
		List<long> subscribeUserIds = await _userSubscribeRepository
			.Select.Where(r => r.CreateUserId == userId)
			.ToListAsync(r => r.SubscribeUserId);
		return subscribeUserIds;
	}

	public PagedResultDto<UserSubscribeDto> GetUserSubscribeeeList(UserSubscribeSearchDto searchDto)
	{
		List<UserSubscribeDto> userSubscribes = _userSubscribeRepository.Select.Include(r => r.SubscribeUser)
			.Where(r => r.CreateUserId == searchDto.UserId)
			.OrderByDescending(r => r.CreateTime)
			.ToPager(searchDto, out long count)
			.ToList(r => new UserSubscribeDto
			{
				CreateUserId = r.CreateUserId.Value,
				SubscribeUserId = r.SubscribeUserId,
				Subscribeer = new OpenUserDto()
				{
					Id = r.SubscribeUser.Id,
					Introduction = r.SubscribeUser.Introduction,
					Nickname = !r.SubscribeUser.IsDeleted ? r.SubscribeUser.Nickname : "该用户已注销",
					Avatar = r.SubscribeUser.Avatar,
					Username = r.SubscribeUser.Username,
				},
				IsSubscribeed = _userSubscribeRepository.Select.Any(r => r.CreateUserId == CurrentUser.FindUserId() && r.SubscribeUserId == r.SubscribeUserId)
			});

		userSubscribes.ForEach(r => { r.Subscribeer.Avatar = _fileRepository.GetFileUrl(r.Subscribeer.Avatar); });

		return new PagedResultDto<UserSubscribeDto>(userSubscribes, count);
	}

	public PagedResultDto<UserSubscribeDto> GetUserFansList(UserSubscribeSearchDto searchDto)
	{
		List<UserSubscribeDto> userSubscribes = _userSubscribeRepository.Select.Include(r => r.LinUser)
			.Where(r => r.SubscribeUserId == searchDto.UserId)
			.OrderByDescending(r => r.CreateTime)
			.ToPager(searchDto, out long count)
			.ToList(r => new UserSubscribeDto
			{
				CreateUserId = r.CreateUserId.Value,
				SubscribeUserId = r.SubscribeUserId,
				Subscribeer = new OpenUserDto()
				{
					Id = r.LinUser.Id,
					Introduction = r.LinUser.Introduction,
					Nickname = !r.LinUser.IsDeleted ? r.LinUser.Nickname : "该用户已注销",
					Avatar = r.LinUser.Avatar,
					Username = r.LinUser.Username,
				},
				//当前登录的用户是否关注了这个粉丝
				IsSubscribeed = _userSubscribeRepository.Select.Any(
					u => u.CreateUserId == CurrentUser.FindUserId() && u.SubscribeUserId == r.CreateUserId)
			});

		userSubscribes.ForEach(r => { r.Subscribeer.Avatar = _fileRepository.GetFileUrl(r.Subscribeer.Avatar); });

		return new PagedResultDto<UserSubscribeDto>(userSubscribes, count);
	}

	public async Task CreateAsync(long subscribeUserId)
	{
		if (subscribeUserId == CurrentUser.FindUserId())
		{
			throw new CMSException("您无法关注自己");
		}

		LinUser linUser = _userRepository.Select.Where(r => r.Id == subscribeUserId).ToOne();
		if (linUser == null)
		{
			throw new CMSException("该用户不存在");
		}

		if (!linUser.IsActive())
		{
			throw new CMSException("该用户已被拉黑");
		}

		bool any = _userSubscribeRepository.Select.Any(r =>
			r.CreateUserId == CurrentUser.FindUserId() && r.SubscribeUserId == subscribeUserId);
		if (any)
		{
			throw new CMSException("您已关注该用户");
		}

		using ICapTransaction capTransaction = UnitOfWorkManager.Current.BeginTransaction(_capBus, false);

		UserSubscribe userSubscribe = new() { SubscribeUserId = subscribeUserId };
		await _userSubscribeRepository.InsertAsync(userSubscribe);

		await _capBus.PublishAsync(CreateNotificationDto.CreateOrCancelAsync, new CreateNotificationDto()
		{
			NotificationType = NotificationType.UserLikeUser,
			NotificationRespUserId = subscribeUserId,
			UserInfoId = CurrentUser.FindUserId() ?? 0,
			CreateTime = DateTime.Now,
		});

		capTransaction.Commit(UnitOfWorkManager.Current);
	}

	public async Task DeleteAsync(long subscribeUserId)
	{
		bool any = await _userSubscribeRepository.Select.AnyAsync(r =>
			r.CreateUserId == CurrentUser.FindUserId() && r.SubscribeUserId == subscribeUserId);
		if (!any)
		{
			throw new CMSException("已取消关注");
		}


		using ICapTransaction capTransaction = UnitOfWorkManager.Current.BeginTransaction(_capBus, false);

		await _userSubscribeRepository.DeleteAsync(r => r.SubscribeUserId == subscribeUserId && r.CreateUserId == CurrentUser.FindUserId());

		await _capBus.PublishAsync(CreateNotificationDto.CreateOrCancelAsync, new CreateNotificationDto()
		{
			NotificationType = NotificationType.UserLikeUser,
			NotificationRespUserId = subscribeUserId,
			UserInfoId = CurrentUser.FindUserId() ?? 0,
			CreateTime = DateTime.Now,
			IsCancel = true
		});

		capTransaction.Commit(UnitOfWorkManager.Current);

	}

}