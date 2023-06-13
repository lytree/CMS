

using CMS.Data.Model.Entities.Base;
using CMS.Data.Repository.Core;
using FreeSql;

namespace CMS.Data.Repository.Dictionary;

public class DictionaryRepository : AdminRepositoryBase<DictEntity>, IDictRepository
{
    public DictionaryRepository(UnitOfWorkManager uowm) : base(uowm)
    {
    }
}