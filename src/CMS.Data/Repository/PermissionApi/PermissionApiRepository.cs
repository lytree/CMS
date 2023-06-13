using CMS.Data.Model.Entities.User;
using CMS.Data.Repository.Core;
using FreeSql;

namespace CMS.Data.Repository.PermissionApi;

public class PermissionApiRepository : AdminRepositoryBase<PermissionApiEntity>, IPermissionApiRepository
{
	public PermissionApiRepository(UnitOfWorkManager uowm) : base(uowm)
	{

	}
}