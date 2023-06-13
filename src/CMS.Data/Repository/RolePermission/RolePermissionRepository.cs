using CMS.Data.Model.Entities.User;
using CMS.Data.Repository.Core;
using FreeSql;

namespace CMS.Data.Repository.RolePermission;

public class RolePermissionRepository : AdminRepositoryBase<RolePermissionEntity>, IRolePermissionRepository
{
	public RolePermissionRepository(UnitOfWorkManager uowm) : base(uowm)
	{

	}
}