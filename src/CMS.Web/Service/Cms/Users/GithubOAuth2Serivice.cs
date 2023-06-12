using AspNet.Security.OAuth.GitHub;
using CMS.Data.Model.Const;
using CMS.Data.Model.Entities.User;
using CMS.Data.Model.Enums;
using CMS.Data.Repository;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;


namespace CMS.Web.Service.Cms.Users;

public class GithubOAuth2Serivice : OAuthService, IOAuth2Service
{
	private readonly IUserRepository _userRepository;
	private readonly IAuditBaseRepository<CMSUserIdentity, long> _userIdentityRepository;

	public GithubOAuth2Serivice(IAuditBaseRepository<CMSUserIdentity, long> userIdentityRepository, IUserRepository userRepository) : base(userIdentityRepository)
	{
		_userIdentityRepository = userIdentityRepository;
		_userRepository = userRepository;
	}


	/// <summary>
	/// 记录授权成功后的信息
	/// </summary>
	/// <param name="principal"></param>
	/// <param name="openId"></param>
	/// <returns></returns>
	public override async Task<long> SaveUserAsync(ClaimsPrincipal principal, string openId)
	{
		CMSUserIdentity linUserIdentity = await _userIdentityRepository.Where(r => r.IdentityType == CMSUserIdentity.GitHub && r.Credential == openId).FirstAsync();

		long userId = 0;
		if (linUserIdentity == null)
		{
			string? email = principal.FindFirst(CMS.Data.Model.Const.ClaimTypes.Email)?.Value;
			string? name = principal.FindFirst(CMS.Data.Model.Const.ClaimTypes.Name)?.Value;
			string gitHubName = principal.FindFirst(GitHubAuthenticationConstants.Claims.Name)?.Value;
			string gitHubApiUrl = principal.FindFirst(GitHubAuthenticationConstants.Claims.Url)?.Value;
			string HtmlUrl = principal.FindFirst(CMSConsts.Claims.HtmlUrl)?.Value;
			string avatarUrl = principal.FindFirst(CMSConsts.Claims.AvatarUrl)?.Value;
			string bio = principal.FindFirst(CMSConsts.Claims.Bio)?.Value;
			string blogAddress = principal.FindFirst(CMSConsts.Claims.BlogAddress)?.Value;

			CMSUser user = new()
			{
				Active = UserStatus.Active,
				Avatar = avatarUrl,
				CreateTime = DateTime.Now,
				LastLoginTime = DateTime.Now,
				Email = email,
				Introduction = bio + HtmlUrl,
				LinUserGroups = new List<CMSUserGroup>()
				{
					new()
					{
						GroupId = CMSConsts.Group.User
					}
				},
				Nickname = gitHubName,
				Username = "",
				BlogAddress = blogAddress,
				LinUserIdentitys = new List<CMSUserIdentity>()
				{
					new(CMSUserIdentity.GitHub,name,openId,DateTime.Now)
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