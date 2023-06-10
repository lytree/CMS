using System.Threading.Tasks;
using CMS.Data.Model.DTO;
using CMS.Data.Model.Entities.User;

namespace CMS.Data.Manager;

/// <summary>
/// Token 处理类
/// </summary>
public interface ITokenManager
{
    Task<TokensDto> CreateTokenAsync(LinUser user);
}