﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNet.Security.OAuth.Gitee;
using LinCms.Common;
using LinCms.Data.Enums;
using IGeekFan.FreeKit.Extras.Dependency;
using IGeekFan.FreeKit.Extras.FreeSql;
using LinCms.Entities;
using LinCms.IRepositories;

namespace CMS.Web.Service.Cms.Users;

[DisableConventionalRegistration]
public class GiteeOAuth2Service : OAuthService, IOAuth2Service
{
	private readonly IUserRepository _userRepository;
	private readonly IAuditBaseRepository<LinUserIdentity> _userIdentityRepository;

	public GiteeOAuth2Service(IAuditBaseRepository<LinUserIdentity> userIdentityRepository, IUserRepository userRepository) : base(userIdentityRepository)
	{
		_userIdentityRepository = userIdentityRepository;
		_userRepository = userRepository;
	}
	public override async Task<long> SaveUserAsync(ClaimsPrincipal principal, string openId)
	{

		LinUserIdentity linUserIdentity = await _userIdentityRepository.Where(r => r.IdentityType == LinUserIdentity.Gitee && r.Credential == openId).FirstAsync();

		long userId = 0;
		if (linUserIdentity == null)
		{

			string email = principal.FindFirst(ClaimTypes.Email)?.Value;
			string name = principal.FindFirst(ClaimTypes.Name)?.Value;

			//string giteeUrl = principal.FindFirst(GiteeAuthenticationConstants.Claims.Url)?.Value;
			string nickname = principal.FindFirst(GiteeAuthenticationConstants.Claims.Name)?.Value;

			string avatarUrl = principal.FindFirst("urn:gitee:avatar_url")?.Value;
			string blogAddress = principal.FindFirst("urn:gitee:blog")?.Value;
			string bio = principal.FindFirst("urn:gitee:bio")?.Value;
			string htmlUrl = principal.FindFirst("urn:gitee:html_url")?.Value;

			LinUser user = new()
			{
				Active = UserStatus.Active,
				Avatar = avatarUrl,
				LastLoginTime = DateTime.Now,
				Email = email,
				Introduction = bio + htmlUrl,
				LinUserGroups = new List<LinUserGroup>()
				{
					new()
					{
						GroupId = LinConsts.Group.User
					}
				},
				Nickname = nickname,
				Username = "",
				BlogAddress = blogAddress,
				LinUserIdentitys = new List<LinUserIdentity>()
				{
					new(LinUserIdentity.Gitee,name,openId,DateTime.Now)
				}
			};
			await _userRepository.InsertAsync(user);
			userId = user.Id;
		}
		else
		{
			if (linUserIdentity.CreateUserId != null) userId = linUserIdentity.CreateUserId.Value;
		}

		return userId;
	}

}