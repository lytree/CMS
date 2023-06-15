using System.Threading.Tasks;
using CMS.Web.Service.Base.Dict.Dto;
using CMS.Web.Model.Dto;

namespace CMS.Web.Service.Base.Dict;

/// <summary>
/// 数据字典接口
/// </summary>
public partial interface IDictService
{
	Task<DictGetOutput> GetAsync(long id);

	Task<PageOutput<DictGetPageOutput>> GetPageAsync(PageInput<DictGetPageDto> input);

	Task<long> AddAsync(DictAddInput input);

	Task UpdateAsync(DictUpdateInput input);

	Task DeleteAsync(long id);

	Task BatchDeleteAsync(long[] ids);

	Task SoftDeleteAsync(long id);

	Task BatchSoftDeleteAsync(long[] ids);
}