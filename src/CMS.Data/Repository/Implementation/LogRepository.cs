using System;
using CMS.Data.Model.Entities.Other;
using FreeSql;

namespace CMS.Data.Repository.Implementation;

public class LogRepository : AuditBaseRepository<LinLog, long>, ILogRepository
{
	public LogRepository(UnitOfWorkManager unitOfWorkManager)
		: base(unitOfWorkManager?.Orm, unitOfWorkManager)
	{

	}

	public void Create(LinLog linlog)
	{
		linlog.CreateTime = DateTime.Now;
		base.Insert(linlog);
	}
}