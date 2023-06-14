using CMS.Web.Service.User.Auth.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace CMS.Web.Service.User.Auth;

/// <summary>
/// 认证授权接口
/// </summary>
public interface IAuthService
{
	string GetToken(AuthLoginOutput user);

	Task<dynamic> LoginAsync(AuthLoginInput input);

	Task<AuthGetUserInfoOutput> GetUserInfoAsync();

	Task<AuthGetPasswordEncryptKeyOutput> GetPasswordEncryptKeyAsync();

	Task<dynamic> Refresh([BindRequired] string token);

}