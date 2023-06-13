using System.Threading.Tasks;

namespace CMS.Data;

/// <summary>
/// 同步数据接口
/// </summary>
public interface ISyncData
{
	Task SyncDataAsync(IFreeSql db, DbConfig dbConfig = null);
}
