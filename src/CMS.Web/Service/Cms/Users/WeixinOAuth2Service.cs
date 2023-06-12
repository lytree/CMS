
using AspNet.Security.OAuth.Weixin;
using CMS.Data.Model.Const;
using CMS.Data.Model.Entities.User;
using CMS.Data.Model.Enums;
using CMS.Data.Repository;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CMS.Web.Service.Cms.Users;


public class WeixinOAuth2Service : OAuthService, IOAuth2Service
{
	private readonly IUserRepository _userRepository;
	private readonly IAuditBaseRepository<CMSUserIdentity,long> _userIdentityRepository;

	public WeixinOAuth2Service(IAuditBaseRepository<CMSUserIdentity, long> userIdentityRepository, IUserRepository userRepository) : base(userIdentityRepository)
	{
		_userIdentityRepository = userIdentityRepository;
		_userRepository = userRepository;
	}
	public override async Task<long> SaveUserAsync(ClaimsPrincipal principal, string unionId)
	{
		CMSUserIdentity linUserIdentity = await _userIdentityRepository.Where(r => r.IdentityType == CMSUserIdentity.Weixin && r.Credential == unionId).FirstAsync();

		long userId = 0;
		if (linUserIdentity == null)
		{

			string gender = principal.FindFirst(CMS.Data.Model.Const.ClaimTypes.Gender)?.Value;
			string nickname = principal.FindFirst(CMS.Data.Model.Const.ClaimTypes.Name)?.Value;

			string openId = principal.FindFirst(WeixinAuthenticationConstants.Claims.OpenId)?.Value;

			string avatarUrl = principal.FindFirst(WeixinAuthenticationConstants.Claims.HeadImgUrl)?.Value;

			CMSUser user = new()
			{
				Active = UserStatus.Active,
				Avatar = avatarUrl,
				LastLoginTime = DateTime.Now,
				Email = "",
				Introduction = "",
				LinUserGroups = new List<CMSUserGroup>()
				{
					new()
					{
						GroupId = CMSConsts.Group.User
					}
				},
				Nickname = nickname,
				Username = "",
				BlogAddress = "",
				LinUserIdentitys = new List<CMSUserIdentity>()
				{
					new(CMSUserIdentity.Weixin,nickname,unionId,DateTime.Now)
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