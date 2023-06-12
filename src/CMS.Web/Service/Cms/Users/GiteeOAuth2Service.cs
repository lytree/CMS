using AspNet.Security.OAuth.Gitee;
using CMS.Data.Model.Const;
using CMS.Data.Model.Entities.User;
using CMS.Data.Model.Enums;
using CMS.Data.Repository;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;


namespace CMS.Web.Service.Cms.Users;


public class GiteeOAuth2Service : OAuthService, IOAuth2Service
{
	private readonly IUserRepository _userRepository;
	private readonly IAuditBaseRepository<CMSUserIdentity,long> _userIdentityRepository;

	public GiteeOAuth2Service(IAuditBaseRepository<CMSUserIdentity,long> userIdentityRepository, IUserRepository userRepository) : base(userIdentityRepository)
	{
		_userIdentityRepository = userIdentityRepository;
		_userRepository = userRepository;
	}
	public override async Task<long> SaveUserAsync(ClaimsPrincipal principal, string openId)
	{

		CMSUserIdentity linUserIdentity = await _userIdentityRepository.Where(r => r.IdentityType == CMSUserIdentity.Gitee && r.Credential == openId).FirstAsync();

		long userId = 0;
		if (linUserIdentity == null)
		{

			string email = principal.FindFirst(CMS.Data.Model.Const.ClaimTypes.Email)?.Value;
			string name = principal.FindFirst(CMS.Data.Model.Const.ClaimTypes.Name)?.Value;

			//string giteeUrl = principal.FindFirst(GiteeAuthenticationConstants.Claims.Url)?.Value;
			string nickname = principal.FindFirst(GiteeAuthenticationConstants.Claims.Name)?.Value;

			string avatarUrl = principal.FindFirst("urn:gitee:avatar_url")?.Value;
			string blogAddress = principal.FindFirst("urn:gitee:blog")?.Value;
			string bio = principal.FindFirst("urn:gitee:bio")?.Value;
			string htmlUrl = principal.FindFirst("urn:gitee:html_url")?.Value;

			CMSUser user = new()
			{
				Active = UserStatus.Active,
				Avatar = avatarUrl,
				LastLoginTime = DateTime.Now,
				Email = email,
				Introduction = bio + htmlUrl,
				LinUserGroups = new List<CMSUserGroup>()
				{
					new()
					{
						GroupId = CMSConsts.Group.User
					}
				},
				Nickname = nickname,
				Username = "",
				BlogAddress = blogAddress,
				LinUserIdentitys = new List<CMSUserIdentity>()
				{
					new(CMSUserIdentity.Gitee,name,openId,DateTime.Now)
				}
			};
			await _userRepository.InsertAsync(user);
			userId = user.Id;
		}
		else
		{
			//if (linUserIdentity.CreateUserId != null) userId = linUserIdentity.CreateUserId.Value;
		}

		return userId;
	}

}