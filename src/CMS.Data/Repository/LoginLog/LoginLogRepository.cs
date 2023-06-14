using CMS.Data.Model.Entities.User;
using CMS.Data.Repository.Core;
using FreeSql;

namespace CMS.Data.Repository.LoginLog;

public class LoginLogRepository : AdminRepositoryBase<LoginLogEntity>, ILoginLogRepository
{
	public LoginLogRepository(UnitOfWorkManager uowm) : base(uowm)
	{
	}
}