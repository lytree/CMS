using CMS.Web.Service.Base.View.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Web.Service.Base.View;

/// <summary>
/// 视图接口
/// </summary>
public interface IViewService
{
	Task<ViewGetOutput> GetAsync(long id);

	Task<List<ViewListOutput>> GetListAsync(string key);

	Task<long> AddAsync(ViewAddInput input);

	Task UpdateAsync(ViewUpdateInput input);

	Task DeleteAsync(long id);

	Task BatchDeleteAsync(long[] ids);

	Task SoftDeleteAsync(long id);

	Task BatchSoftDeleteAsync(long[] ids);

	Task SyncAsync(ViewSyncInput input);
}