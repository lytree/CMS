using CMS.Data.Model.Entities.Other;
using CMS.Data.Repository;
using FreeSql;
using System.Linq.Expressions;

namespace CMS.Data.Repository
{
	public interface IFileRepository : IAuditBaseRepository<LinFile, long>
	{

		string GetFileUrl(string path);

	}
}


