using CMS.Data.Model.Entities.User;
using CMS.Data.Repository.Core;
using FreeSql;

namespace CMS.Data.Repository.View;

public class ViewRepository : AdminRepositoryBase<ViewEntity>, IViewRepository
{
	public ViewRepository(UnitOfWorkManager muowm) : base(muowm)
	{
	}
}