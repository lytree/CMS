using CMS.Data.Model.Entities.Base;
using CMS.Data.Repository.Core;
using FreeSql;

namespace CMS.Data.Repository.File;

public class FileRepository : AdminRepositoryBase<FileEntity>, IFileRepository
{
	public FileRepository(UnitOfWorkManager uowm) : base(uowm)
	{
	}
}