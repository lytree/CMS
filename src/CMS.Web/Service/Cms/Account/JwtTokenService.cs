﻿using System;
using System.Threading.Tasks;
using CMS.Data.Exceptions;
using CMS.Data.Model.Entities.User;
using CMS.Data.Model.Enums;
using CMS.Data.Repository;
using CMS.Web.Service.Cms.Users;
using Microsoft.Extensions.Logging;

namespace CMS.Web.Service.Cms.Account;

public class JwtTokenService : ITokenService
{
	private readonly IUserRepository _userRepository;
	private readonly IUserIdentityService _userIdentityService;
	private readonly ILogger<JwtTokenService> _logger;
	private readonly ITokenManager _tokenManager;

	public JwtTokenService(IUserRepository userRepository, ILogger<JwtTokenService> logger, IUserIdentityService userIdentityService, ITokenManager tokenManager)
	{
		_userRepository = userRepository;
		_logger = logger;
		_userIdentityService = userIdentityService;
		_tokenManager = tokenManager;
	}
	/// <summary>
	/// JWT登录
	/// </summary>
	/// <param name="loginInputDto"></param>
	/// <returns></returns>
	public async Task<Tokens> LoginAsync(LoginInputDto loginInputDto)
	{
		_logger.LogInformation("JwtLogin");

		CMSUser user = await _userRepository.GetUserAsync(r => r.Username == loginInputDto.Username || r.Email == loginInputDto.Username);

		if (user == null)
		{
			throw new CMSException("用户不存在", ErrorCode.NotFound);
		}

		if (user.Active == UserStatus.NotActive)
		{
			throw new CMSException("用户未激活", ErrorCode.NoPermission);
		}

		bool valid = await _userIdentityService.VerifyUserPasswordAsync(user.Id, loginInputDto.Password, user.Salt);

		if (!valid)
		{
			throw new CMSException("请输入正确密码", ErrorCode.ParameterError);
		}

		_logger.LogInformation($"用户{loginInputDto.Username},登录成功");

		Tokens tokens = await _tokenManager.CreateTokenAsync(user);
		return tokens;
	}


	public async Task<Tokens> GetRefreshTokenAsync(string refreshToken)
	{
		CMSUser user = await _userRepository.GetUserAsync(r => r.RefreshToken == refreshToken);

		if (user.IsNull())
		{
			throw new CMSException("该refreshToken无效!");
		}

		if (DateTime.Compare(user.LastLoginTime, DateTime.Now) > new TimeSpan(30, 0, 0, 0).Ticks)
		{
			throw new CMSException("请重新登录", ErrorCode.RefreshTokenError);
		}

		Tokens tokens = await _tokenManager.CreateTokenAsync(user);
		_logger.LogInformation($"用户{user.Username},JwtRefreshToken 刷新-登录成功");

		return tokens;
	}

}