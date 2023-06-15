using System.Threading.Tasks;
using CMS.Web.Service.Base.DictType.Dto;
using CMS.Web.Model.Dto;

namespace CMS.Web.Service.Base.DictType;

/// <summary>
/// 数据字典类型接口
/// </summary>
public partial interface IDictTypeService
{
	Task<DictTypeGetOutput> GetAsync(long id);

	Task<PageOutput<DictTypeGetPageOutput>> GetPageAsync(PageInput<DictTypeGetPageDto> input);

	Task<long> AddAsync(DictTypeAddInput input);

	Task UpdateAsync(DictTypeUpdateInput input);

	Task DeleteAsync(long id);

	Task BatchDeleteAsync(long[] ids);

	Task SoftDeleteAsync(long id);

	Task BatchSoftDeleteAsync(long[] ids);
}