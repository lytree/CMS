using CMS.Web.Model.Dto;
using CMS.Web.Service.User.Auth.Dto;
using CMS.Web.Service.User.User.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Web.Service.User.User;

/// <summary>
/// 用户接口
/// </summary>
public interface IUserService
{
	Task<UserGetOutput> GetAsync(long id);

	Task<PageOutput<UserGetPageOutput>> GetPageAsync(PageInput<UserGetPageDto> input);

	Task<AuthLoginOutput> GetLoginUserAsync(long id);

	Task<long> AddAsync(UserAddInput input);

	Task<long> AddMemberAsync(UserAddMemberInput input);

	Task UpdateAsync(UserUpdateInput input);

	Task DeleteAsync(long id);

	Task BatchDeleteAsync(long[] ids);

	Task SoftDeleteAsync(long id);

	Task BatchSoftDeleteAsync(long[] ids);

	Task ChangePasswordAsync(UserChangePasswordInput input);

	Task<string> ResetPasswordAsync(UserResetPasswordInput input);

	Task UpdateBasicAsync(UserUpdateBasicInput input);

	Task<UserGetBasicOutput> GetBasicAsync();

	Task<IList<UserPermissionsOutput>> GetPermissionsAsync();

	Task<string> AvatarUpload([FromForm] IFormFile file, bool autoUpdate = false);

	Task<dynamic> OneClickLoginAsync(string userName);
}