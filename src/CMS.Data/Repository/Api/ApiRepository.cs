

using CMS.Data.Model.Entities.User;
using CMS.Data.Repository.Core;
using FreeSql;

namespace CMS.Data.Repository.Api;

public class ApiRepository : AdminRepositoryBase<ApiEntity>, IApiRepository
{
    public ApiRepository(UnitOfWorkManager uowm) : base(uowm)
    {
    }
}