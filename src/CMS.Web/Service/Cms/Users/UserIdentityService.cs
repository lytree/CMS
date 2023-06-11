using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Data.Exceptions;
using CMS.Data.Model.Entities.User;
using CMS.Data.Repository;
using DotNetCore.Security;


namespace CMS.Web.Service.Cms.Users;

public class UserIdentityService : ApplicationService, IUserIdentityService
{
	private readonly IAuditBaseRepository<LinUserIdentity,long> _userIdentityRepository;
	private readonly ICryptographyService _cryptographyService;
	public UserIdentityService(IAuditBaseRepository<LinUserIdentity,long> userIdentityRepository, ICryptographyService cryptographyService)
	{
		_userIdentityRepository = userIdentityRepository;
		_cryptographyService = cryptographyService;
	}

	public async Task<bool> VerifyUserPasswordAsync(long userId, string password, string salt)
	{
		LinUserIdentity userIdentity = await GetFirstByUserIdAsync(userId);
		//快速登录时，用户实际未设置密码
		if (userIdentity == null)
		{
			return true;
		}
		string encryptPassword = _cryptographyService.Encrypt(password, salt);
		return userIdentity.Credential == encryptPassword;
	}


	public async Task ChangePasswordAsync(long userId, string newpassword, string salt)
	{
		var linUserIdentity = await GetFirstByUserIdAsync(userId); ;

		await ChangePasswordAsync(linUserIdentity, newpassword, salt);
	}


	public Task ChangePasswordAsync(LinUserIdentity linUserIdentity, string newpassword, string salt)
	{
		string encryptPassword = _cryptographyService.Encrypt(newpassword, salt);
		if (linUserIdentity == null)
		{
			linUserIdentity = new LinUserIdentity(LinUserIdentity.Password, "", encryptPassword, DateTime.Now);
			return _userIdentityRepository.InsertAsync(linUserIdentity);
		}
		else
		{
			linUserIdentity.Credential = encryptPassword;
			return _userIdentityRepository.UpdateAsync(linUserIdentity);
		}
	}

	public Task DeleteAsync(long userId)
	{
		return _userIdentityRepository.Where(r => r.CreateUserId == userId).ToDelete().ExecuteAffrowsAsync();
	}

	public Task<LinUserIdentity> GetFirstByUserIdAsync(long userId)
	{
		return _userIdentityRepository
			.Where(r => r.CreateUserId == userId && r.IdentityType == LinUserIdentity.Password)
			.FirstAsync();
	}

	public async Task<List<UserIdentityDto>> GetListAsync(long userId)
	{
		List<LinUserIdentity> userIdentities = await _userIdentityRepository
			.Where(r => r.CreateUserId == userId)
			.ToListAsync();

		return Mapper.Map<List<UserIdentityDto>>(userIdentities);
	}

	public async Task UnBind(long id)
	{
		LinUserIdentity userIdentity = await _userIdentityRepository.GetAsync(id);
		if (userIdentity == null || userIdentity.CreateUserId != CurrentUser.FindUserId())
		{
			throw new CMSException("你无权解绑此账号");
		}

		List<LinUserIdentity> userIdentities = await _userIdentityRepository.Select.Where(r => r.CreateUserId == CurrentUser.FindUserId()).ToListAsync();

		bool hasPwd = userIdentities.Any(r => r.IdentityType == LinUserIdentity.Password);

		if (!hasPwd && userIdentities.Count == 1)
		{
			throw new CMSException("你未设置密码，无法解绑最后一个第三方登录账号");
		}
		await _userIdentityRepository.DeleteAsync(userIdentity);
	}
}