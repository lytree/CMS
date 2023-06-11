using CMS.Data.Model.Entities;
using CMS.Data.Options;
using FreeSql;
using Microsoft.Extensions.Options;

namespace CMS.Data.Repository.Implementation;

public class FileRepository : AuditBaseRepository<LinFile, long>, IFileRepository
{
	private readonly FileStorageOption _fileStorageOption;
	public FileRepository(UnitOfWorkManager unitOfWorkManager, IOptionsMonitor<FileStorageOption> fileStorageOption) : base(unitOfWorkManager?.Orm, unitOfWorkManager)
	{
		_fileStorageOption = fileStorageOption.CurrentValue;
	}

	public string GetFileUrl(string path)
	{
		if (string.IsNullOrEmpty(path)) return "";
		if (path.StartsWith("http") || path.StartsWith("https"))
		{
			return path;
		}
		return _fileStorageOption.LocalFile.Host + path;

		//string redisKey = "filerepository:getfileurl:" +path;

		//return  RedisHelper.CacheShell(
		//    redisKey, 5*60, () =>
		//    {
		//        LinFile linFile = Where(r => r.Path == path).First();
		//        if (linFile == null) return path;
		//        return linFile.Type switch
		//        {
		//            1 => _fileStorageOption.LocalFile.Host + path,
		//            2 => _fileStorageOption.Qiniu.Host + path,
		//            _ => path,
		//        };
		//    }
		// );
	}
}