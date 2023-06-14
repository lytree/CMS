using System.Threading.Tasks;
using System.Collections.Generic;
using CMS.Web.Service.Base.Api.Dto;
using CMS.Web.Model.Dto;
using CMS.Data.Model.Entities.Base;

namespace CMS.Web.Service.Base.Api;

/// <summary>
/// api接口
/// </summary>
public interface IApiService
{
	Task<ApiGetOutput> GetAsync(long id);

	Task<List<ApiListOutput>> GetListAsync(string key);

	Task<PageOutput<ApiEntity>> GetPageAsync(PageInput<ApiGetPageDto> input);

	Task<long> AddAsync(ApiAddInput input);

	Task UpdateAsync(ApiUpdateInput input);

	Task DeleteAsync(long id);

	Task BatchDeleteAsync(long[] ids);

	Task SoftDeleteAsync(long id);

	Task BatchSoftDeleteAsync(long[] ids);

	Task SyncAsync(ApiSyncInput input);
}