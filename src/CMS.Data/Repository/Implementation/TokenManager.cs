using CMS.Data.Model.Const;
using CMS.Data.Model.Entities.User;
using CMS.Data.Repository;
using CMS.Data.Security;
using DotNetCore.Security;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CMS.Data.Repository.Implementation;

public class TokenManager : ITokenManager
{
    private readonly ILogger<TokenManager> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jsonWebTokenService;
    public TokenManager(IJwtService jsonWebTokenService, ILogger<TokenManager> logger, IUserRepository userRepository)
    {
        _jsonWebTokenService = jsonWebTokenService;
        _logger = logger;
        _userRepository = userRepository;
    }

    public async Task<Tokens> CreateTokenAsync(LinUser user)
    {
        List<Claim> claims = new()
        {
            new Claim (Model.Const.ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim (Model.Const.ClaimTypes.Email, user.Email?? ""),
            new Claim (Model.Const.ClaimTypes.Name, user.Nickname?? ""),
            new Claim (Model.Const.ClaimTypes.UserName, user.Username?? ""),
        };
        user.LinGroups?.ForEach(r =>
        {
            claims.Add(new Claim(Model.Const.ClaimTypes.Role, r.Name));
            claims.Add(new Claim(LinCmsClaimTypes.GroupIds, r.Id.ToString()));
        });

        string token = _jsonWebTokenService.Encode(claims);

        user.AddRefreshToken();
        await _userRepository.UpdateAsync(user);

        return new Tokens(token, user.RefreshToken);
    }
}