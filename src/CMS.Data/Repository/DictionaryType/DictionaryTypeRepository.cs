using CMS.Data.Model.Entities.Base;
using CMS.Data.Repository.Core;
using FreeSql;

namespace CMS.Data.Repository.DictionaryType;

public class DictionaryTypeRepository : AdminRepositoryBase<DictTypeEntity>, IDictTypeRepository
{
	public DictionaryTypeRepository(UnitOfWorkManager uowm) : base(uowm)
	{
	}
}