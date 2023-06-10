﻿using CMS.Data.Model.Entities.Other;
using CMS.Data.Repository;
using FreeSql;
using System.Linq.Expressions;

namespace CMS.Data.Repository
{
	public interface ILogRepository : IAuditBaseRepository<LinLog,long>
	{

		void Create(LinLog linlog);
	}
}
