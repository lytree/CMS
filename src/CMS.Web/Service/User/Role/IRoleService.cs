using CMS.Web.Model.Dto;
using CMS.Web.Service.User.Role.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Web.Service.User.Role;

/// <summary>
/// 角色接口
/// </summary>
public interface IRoleService
{
	Task<RoleGetOutput> GetAsync(long id);

	Task<List<RoleGetListOutput>> GetListAsync(RoleGetListInput input);

	Task<PageOutput<RoleGetPageOutput>> GetPageAsync(PageInput<RoleGetPageDto> input);

	Task<long> AddAsync(RoleAddInput input);

	Task<List<RoleGetRoleUserListOutput>> GetRoleUserListAsync([FromQuery] RoleGetRoleUserListInput input);

	Task AddRoleUserAsync(RoleAddRoleUserListInput input);

	Task RemoveRoleUserAsync(RoleAddRoleUserListInput input);

	Task UpdateAsync(RoleUpdateInput input);

	Task DeleteAsync(long id);

	Task BatchDeleteAsync(long[] ids);

	Task SoftDeleteAsync(long id);

	Task BatchSoftDeleteAsync(long[] ids);

}