using CMS.Data.Model.Entities.Base;
using CMS.Data.Repository.Core;
using FreeSql;

namespace CMS.Data.Repository.OprationLog;

public class OprationLogRepository : AdminRepositoryBase<OprationLogEntity>, IOprationLogRepository
{
	public OprationLogRepository(UnitOfWorkManager uowm) : base(uowm)
	{
	}
}