using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.Model.DTO;


/// <summary>
/// 访问令牌
/// </summary>
[Serializable]
public class TokensDto
{
    public TokensDto(string accessToken, string refreshToken)
    {
        AccessToken = accessToken ?? throw new ArgumentNullException(nameof(accessToken));
        RefreshToken = refreshToken ?? throw new ArgumentNullException(nameof(refreshToken));
    }

    /// <summary>
    /// 授权接口调用凭证
    /// </summary>
    /// <value></value>
    public string AccessToken { get; private set; }

    /// <summary>
    /// 用户刷新AccessToken
    /// </summary>
    /// <value></value>
    public string RefreshToken { get; private set; }

    public override string ToString()
    {
        return $"Tokens AccessToken:{AccessToken},RefreshToken:{RefreshToken}";
    }
}
