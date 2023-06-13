using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using CMS.Data.Model.Entities.User;
using CMS.Data.Model.Entities.Base;
using CMS.Common.Extensions;
namespace CMS.Data.Repository.Core;

/// <summary>
/// 生成数据
/// </summary>
public class CustomGenerateData : GenerateData, IGenerateData
{
	public virtual async Task GenerateDataAsync(IFreeSql db)
	{
		#region 读取数据

		//admin
		#region 数据字典

		var dictionaryTypes = await db.Queryable<DictTypeEntity>().ToListAsync();

		var dictionaries = await db.Queryable<DictEntity>().ToListAsync();
		#endregion

		#region 视图

		var views = await db.Queryable<ViewEntity>().ToListAsync();
		var viewTree = views.Clone().ToTree((r, c) =>
		{
			return c.ParentId == 0;
		},
	   (r, c) =>
	   {
		   return r.Id == c.ParentId;
	   },
	   (r, datalist) =>
	   {
		   r.Childs ??= new List<ViewEntity>();
		   r.Childs.AddRange(datalist);
	   });

		#endregion

		#region 权限

		var permissions = await db.Queryable<PermissionEntity>().ToListAsync();
		var permissionTree = permissions.Clone().ToTree((r, c) =>
		{
			return c.ParentId == 0;
		},
	   (r, c) =>
	   {
		   return r.Id == c.ParentId;
	   },
	   (r, datalist) =>
	   {
		   r.Childs ??= new List<PermissionEntity>();
		   r.Childs.AddRange(datalist);
	   });

		#endregion

		#region 用户

		var users = await db.Queryable<UserEntity>().ToListAsync();

		#endregion

		#region 角色

		var roles = await db.Queryable<RoleEntity>().ToListAsync();

		#endregion

		#region 用户角色

		var userRoles = await db.Queryable<UserRoleEntity>().ToListAsync();

		#endregion

		#region 角色权限

		var rolePermissions = await db.Queryable<RolePermissionEntity>().ToListAsync();
		#endregion




		#endregion


		#region 生成数据

		SaveDataToJsonFile<UserEntity>(users);
		SaveDataToJsonFile<RoleEntity>(roles);
		SaveDataToJsonFile<DictEntity>(dictionaries);
		SaveDataToJsonFile<DictTypeEntity>(dictionaryTypes);
		SaveDataToJsonFile<UserRoleEntity>(userRoles);
		SaveDataToJsonFile<ViewEntity>(viewTree);
		SaveDataToJsonFile<PermissionEntity>(permissionTree);
		SaveDataToJsonFile<RolePermissionEntity>(rolePermissions);
		#endregion
	}
}