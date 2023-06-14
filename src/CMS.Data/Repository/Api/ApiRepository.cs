

using CMS.Data.Model.Entities.Base;
using CMS.Data.Repository.Core;
using FreeSql;

namespace CMS.Data.Repository.Api;

public class ApiRepository : AdminRepositoryBase<ApiEntity>, IApiRepository
{
    public ApiRepository(UnitOfWorkManager uowm) : base(uowm)
    {
    }
}