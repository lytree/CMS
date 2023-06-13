using CMS.Data.Model.Const;
using FreeSql;

namespace CMS.Data.Repository.Core
{
    /// <summary>
    /// 权限库基础仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class AdminRepositoryBase<TEntity> : RepositoryBase<TEntity> where TEntity : class
    {
        public AdminRepositoryBase(UnitOfWorkManager uowm) : base(uowm)
        {

        }
    }
}