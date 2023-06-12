using System.Threading.Tasks;
using CMS.Data.Model.Entities.User;

namespace CMS.Data.Repository;

/// <summary>
/// Token 处理类
/// </summary>
public interface ITokenManager
{
    Task<Tokens> CreateTokenAsync(CMSUser user);
}