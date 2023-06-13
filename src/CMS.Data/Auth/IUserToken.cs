using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CMS.Data.Auth;

public interface IUserToken
{
	string Create(Claim[] claims);

	JwtSecurityToken Decode(string jwtToken);
}