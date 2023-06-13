using CMS.Data.Model.Entities.User;
using CMS.Data.Repository.Core;
using FreeSql;

namespace CMS.Data.Repository.UserRole;

public class UserRoleRepository : AdminRepositoryBase<UserRoleEntity>, IUserRoleRepository
{
	public UserRoleRepository(UnitOfWorkManager uowm) : base(uowm)
	{

	}
}