using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CMS.Data.Model.Entities;
using CMS.Data.Repository;
using FreeSql;

namespace CMS.Data.Repository
{
	public interface ISettingRepository : IAuditBaseRepository<CMSSetting,long>
	{

		Task<CMSSetting> FindAsync(string name, string providerName, string providerKey);

		Task<List<CMSSetting>> GetListAsync(string providerName, string providerKey);
	}
}
