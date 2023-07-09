using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using CMS.Web.Service.User.Permission.Dto;
using CMS.Web.Model.Dto;
using CMS.DynamicApi;
using CMS.Web.Model.Consts;
using CMS.DynamicApi.Attributes;
using CMS.Web.Config;
using CMS.Data.Repository.Permission;
using CMS.Data.Repository.User;
using CMS.Data.Repository.Role;
using CMS.Data.Repository.RolePermission;
using CMS.Data.Repository.UserRole;
using CMS.Data.Repository.PermissionApi;
using CMS.Data.Model.Entities.User;
using CMS.Common.Extensions;
using CMS.Data.Attributes;

namespace CMS.Web.Service.User.Permission;

/// <summary>
/// 权限服务
/// </summary>
[Order(40)]
[DynamicApi(Area = AdminConsts.AreaName)]
public class PermissionService : BaseService, IPermissionService, IDynamicApi
{
	private AppConfig _appConfig => LazyGetRequiredService<AppConfig>();
	private IPermissionRepository _permissionRepository => LazyGetRequiredService<IPermissionRepository>();
	private IRoleRepository _roleRepository => LazyGetRequiredService<IRoleRepository>();
	private IUserRepository _userRepository => LazyGetRequiredService<IUserRepository>();
	private IRolePermissionRepository _rolePermissionRepository => LazyGetRequiredService<IRolePermissionRepository>();
	private IUserRoleRepository _userRoleRepository => LazyGetRequiredService<IUserRoleRepository>();
	private IPermissionApiRepository _permissionApiRepository => LazyGetRequiredService<IPermissionApiRepository>();

	public PermissionService()
	{
	}

	/// <summary>
	/// 清除权限下关联的用户权限缓存
	/// </summary>
	/// <param name="permissionIds"></param>
	/// <returns></returns>
	private async Task ClearUserPermissionsAsync(List<long> permissionIds)
	{
		var userIds = await _userRoleRepository.Select.Where(a =>
			_rolePermissionRepository
			.Where(b => b.RoleId == a.RoleId && permissionIds.Contains(b.PermissionId))
			.Any()
		).ToListAsync(a => a.UserId);
		foreach (var userId in userIds)
		{
			await Cache.DelAsync(CacheKeys.UserPermissions + userId);
		}
	}

	/// <summary>
	/// 查询分组
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	public async Task<PermissionGetGroupOutput> GetGroupAsync(long id)
	{
		var result = await _permissionRepository.GetAsync<PermissionGetGroupOutput>(id);
		return result;
	}

	/// <summary>
	/// 查询菜单
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	public async Task<PermissionGetMenuOutput> GetMenuAsync(long id)
	{
		var result = await _permissionRepository.GetAsync<PermissionGetMenuOutput>(id);
		return result;
	}

	/// <summary>
	/// 查询接口
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	public async Task<PermissionGetApiOutput> GetApiAsync(long id)
	{
		var result = await _permissionRepository.GetAsync<PermissionGetApiOutput>(id);
		return result;
	}

	/// <summary>
	/// 查询权限点
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	public async Task<PermissionGetDotOutput> GetDotAsync(long id)
	{
		var output = await _permissionRepository.Select
		.WhereDynamic(id)
		.ToOneAsync(a => new PermissionGetDotOutput
		{
			ApiIds = _permissionApiRepository.Where(b => b.PermissionId == a.Id).OrderBy(a => a.Id).ToList(b => b.Api.Id)
		});
		return output;
	}

	/// <summary>
	/// 查询权限列表
	/// </summary>
	/// <param name="key"></param>
	/// <param name="start"></param>
	/// <param name="end"></param>
	/// <returns></returns>
	public async Task<List<PermissionListOutput>> GetListAsync(string? key, DateTime? start, DateTime? end)
	{
		if (end.HasValue)
		{
			end = end.Value.AddDays(1);
		}

		var data = await _permissionRepository
			.WhereIf(key.NotNull(), a => a.Path.Contains(key) || a.Label.Contains(key))
			.WhereIf(start.HasValue && end.HasValue, a => a.CreatedTime.Value.BetweenEnd(start.Value, end.Value))
			.Include(a => a.View)
			.OrderBy(a => new { a.ParentId, a.Sort })
			.ToListAsync(a => new PermissionListOutput
			{
				ViewPath = a.View.Path,
				ApiPaths = string.Join(";", _permissionApiRepository.Where(b => b.PermissionId == a.Id).ToList(b => b.Api.Path))
			});

		return data;
	}

	/// <summary>
	/// 查询授权权限列表
	/// </summary>
	/// <returns></returns>
	public async Task<IEnumerable<dynamic>> GetPermissionListAsync()
	{
		var permissions = await _permissionRepository.Select
			.AsTreeCte(up: true)
			.ToListAsync(a => new { a.Id, a.ParentId, a.Label, a.Type, a.Sort });

		var menus = permissions.DistinctBy(a => a.Id).OrderBy(a => a.ParentId).ThenBy(a => a.Sort)
			.Select(a => new
			{
				a.Id,
				a.ParentId,
				a.Label,
				Row = a.Type == PermissionType.Menu
			});

		return menus;
	}

	/// <summary>
	/// 查询角色权限列表
	/// </summary>
	/// <param name="roleId"></param>
	/// <returns></returns>
	public async Task<List<long>> GetRolePermissionListAsync(long roleId = 0)
	{
		var permissionIds = await _rolePermissionRepository
			.Select.Where(d => d.RoleId == roleId)
			.ToListAsync(a => a.PermissionId);

		return permissionIds;
	}

	

	/// <summary>
	/// 新增分组
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	public async Task<long> AddGroupAsync(PermissionAddGroupInput input)
	{
		var entity = Mapper.Map<PermissionEntity>(input);
		entity.Type = PermissionType.Group;

		if (entity.Sort == 0)
		{
			var sort = await _permissionRepository.Select.Where(a => a.ParentId == input.ParentId).MaxAsync(a => a.Sort);
			entity.Sort = sort + 1;
		}

		await _permissionRepository.InsertAsync(entity);
		return entity.Id;
	}

	/// <summary>
	/// 新增菜单
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	public async Task<long> AddMenuAsync(PermissionAddMenuInput input)
	{
		var entity = Mapper.Map<PermissionEntity>(input);
		entity.Type = PermissionType.Menu;
		if (entity.Sort == 0)
		{
			var sort = await _permissionRepository.Select.Where(a => a.ParentId == input.ParentId).MaxAsync(a => a.Sort);
			entity.Sort = sort + 1;
		}
		await _permissionRepository.InsertAsync(entity);

		return entity.Id;
	}

	/// <summary>
	/// 新增接口
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	public async Task<long> AddApiAsync(PermissionAddApiInput input)
	{
		var entity = Mapper.Map<PermissionEntity>(input);
		entity.Type = PermissionType.Dot;
		if (entity.Sort == 0)
		{
			var sort = await _permissionRepository.Select.Where(a => a.ParentId == input.ParentId).MaxAsync(a => a.Sort);
			entity.Sort = sort + 1;
		}
		await _permissionRepository.InsertAsync(entity);

		return entity.Id;
	}

	/// <summary>
	/// 新增权限点
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	[AdminTransaction]
	public virtual async Task<long> AddDotAsync(PermissionAddDotInput input)
	{
		var entity = Mapper.Map<PermissionEntity>(input);
		entity.Type = PermissionType.Dot;
		if (entity.Sort == 0)
		{
			var sort = await _permissionRepository.Select.Where(a => a.ParentId == input.ParentId).MaxAsync(a => a.Sort);
			entity.Sort = sort + 1;
		}
		await _permissionRepository.InsertAsync(entity);

		if (input.ApiIds != null && input.ApiIds.Any())
		{
			var permissionApis = input.ApiIds.Select(a => new PermissionApiEntity { PermissionId = entity.Id, ApiId = a }).ToList();
			await _permissionApiRepository.InsertAsync(permissionApis);
		}

		return entity.Id;
	}

	/// <summary>
	/// 修改分组
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	public async Task UpdateGroupAsync(PermissionUpdateGroupInput input)
	{
		var entity = await _permissionRepository.GetAsync(input.Id);
		entity = Mapper.Map(input, entity);
		await _permissionRepository.UpdateAsync(entity);
	}

	/// <summary>
	/// 修改菜单
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	public async Task UpdateMenuAsync(PermissionUpdateMenuInput input)
	{
		var entity = await _permissionRepository.GetAsync(input.Id);
		entity = Mapper.Map(input, entity);
		await _permissionRepository.UpdateAsync(entity);
	}

	/// <summary>
	/// 修改接口
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	public async Task UpdateApiAsync(PermissionUpdateApiInput input)
	{
		var entity = await _permissionRepository.GetAsync(input.Id);
		entity = Mapper.Map(input, entity);
		await _permissionRepository.UpdateAsync(entity);
	}

	/// <summary>
	/// 修改权限点
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	[AdminTransaction]
	public virtual async Task UpdateDotAsync(PermissionUpdateDotInput input)
	{
		var entity = await _permissionRepository.GetAsync(input.Id);
		if (!(entity?.Id > 0))
		{
			throw ResultOutput.Exception("权限点不存在！");
		}

		Mapper.Map(input, entity);
		await _permissionRepository.UpdateAsync(entity);
		await _permissionApiRepository.DeleteAsync(a => a.PermissionId == entity.Id);

		if (input.ApiIds != null && input.ApiIds.Any())
		{
			var permissionApis = input.ApiIds.Select(a => new PermissionApiEntity { PermissionId = entity.Id, ApiId = a });
			await _permissionApiRepository.InsertAsync(permissionApis.ToList());
		}

		//清除用户权限缓存
		await ClearUserPermissionsAsync(new List<long> { entity.Id });
	}

	/// <summary>
	/// 彻底删除
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	[AdminTransaction]
	public virtual async Task DeleteAsync(long id)
	{
		//递归查询所有权限点
		var ids = _permissionRepository.Select
		.Where(a => a.Id == id)
		.AsTreeCte()
		.ToList(a => a.Id);

		//删除权限关联接口
		await _permissionApiRepository.DeleteAsync(a => ids.Contains(a.PermissionId));

		//删除相关权限
		await _permissionRepository.DeleteAsync(a => ids.Contains(a.Id));

		//清除用户权限缓存
		await ClearUserPermissionsAsync(ids);
	}

	/// <summary>
	/// 删除
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	public async Task SoftDeleteAsync(long id)
	{
		//递归查询所有权限点
		var ids = _permissionRepository.Select
		.Where(a => a.Id == id)
		.AsTreeCte()
		.ToList(a => a.Id);

		//删除权限
		await _permissionRepository.SoftDeleteAsync(a => ids.Contains(a.Id));

		//清除用户权限缓存
		await ClearUserPermissionsAsync(ids);
	}

	/// <summary>
	/// 保存角色权限
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	[AdminTransaction]
	public virtual async Task AssignAsync(PermissionAssignInput input)
	{
		//分配权限的时候判断角色是否存在
		var exists = await _roleRepository.Select.DisableGlobalFilter(FilterNames.Tenant).WhereDynamic(input.RoleId).AnyAsync();
		if (!exists)
		{
			throw ResultOutput.Exception("该角色不存在或已被删除！");
		}

		//查询角色权限
		var permissionIds = await _rolePermissionRepository.Select.Where(d => d.RoleId == input.RoleId).ToListAsync(m => m.PermissionId);

		//批量删除权限
		var deleteIds = permissionIds.Where(d => !input.PermissionIds.Contains(d));
		if (deleteIds.Any())
		{
			await _rolePermissionRepository.DeleteAsync(m => m.RoleId == input.RoleId && deleteIds.Contains(m.PermissionId));
		}

		//批量插入权限
		var insertRolePermissions = new List<RolePermissionEntity>();
		var insertPermissionIds = input.PermissionIds.Where(d => !permissionIds.Contains(d));

		if (insertPermissionIds.Any())
		{
			foreach (var permissionId in insertPermissionIds)
			{
				insertRolePermissions.Add(new RolePermissionEntity()
				{
					RoleId = input.RoleId,
					PermissionId = permissionId,
				});
			}
			await _rolePermissionRepository.InsertAsync(insertRolePermissions);
		}

		//清除角色下关联的用户权限缓存
		var userIds = await _userRoleRepository.Select.Where(a => a.RoleId == input.RoleId).ToListAsync(a => a.UserId);
		foreach (var userId in userIds)
		{
			await Cache.DelAsync(CacheKeys.UserPermissions + userId);
		}
	}
}