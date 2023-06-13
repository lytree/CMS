using CMS.Data.Model.Entities.User;
using CMS.Data.Repository.Core;
using FreeSql;

namespace CMS.Data.Repository.Permission;

public class PermissionRepository : AdminRepositoryBase<PermissionEntity>, IPermissionRepository
{
	public PermissionRepository(UnitOfWorkManager uowm) : base(uowm)
	{
	}
}