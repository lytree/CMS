using CMS.Data.Model.Entities.User;
using CMS.Data.Repository.Core;
using FreeSql;

namespace CMS.Data.Repository.User;

public class UserRepository : AdminRepositoryBase<UserEntity>, IUserRepository
{
	public UserRepository(UnitOfWorkManager muowm) : base(muowm)
	{

	}
}