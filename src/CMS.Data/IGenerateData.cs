using System.Threading.Tasks;

namespace CMS.Data;

/// <summary>
/// 生成数据接口
/// </summary>
public interface IGenerateData
{
	Task GenerateDataAsync(IFreeSql db);
}
