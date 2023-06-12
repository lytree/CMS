using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CMS.Data.Model.Entities;
using FreeSql;

namespace CMS.Data.Repository.Implementation;

public class SettingRepository : AuditBaseRepository<CMSSetting, long>, ISettingRepository
{
	public SettingRepository(UnitOfWorkManager unitOfWorkManager) : base(unitOfWorkManager?.Orm, unitOfWorkManager)
	{
	}

	public async Task<CMSSetting> FindAsync(string name, string providerName, string providerKey)
	{
		return await Select.Where(s => s.Name == name && s.ProviderName == providerName && s.ProviderKey == providerKey)
			.FirstAsync();
	}

	public async Task<List<CMSSetting>> GetListAsync(string providerName, string providerKey)
	{
		return await Select
			.Where(
				s => s.ProviderName == providerName && s.ProviderKey == providerKey
			).ToListAsync();
	}
}