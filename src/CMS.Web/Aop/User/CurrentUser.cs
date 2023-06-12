using CMS.Web.Extensions;
using System.Security.Claims;

namespace CMS.Web.Aop.User
{
    public class CurrentUser
    {
        /// <summary>
        /// 证件持有者
        /// </summary>
        protected readonly ClaimsPrincipal ClaimsPrincipal;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            ClaimsPrincipal = (httpContextAccessor.HttpContext?.User ?? Thread.CurrentPrincipal as ClaimsPrincipal) ?? new ClaimsPrincipal();
        }
        /// <inheritdoc />
        public virtual bool IsAuthenticated => ClaimsPrincipal.FindUserId() != null;

        /// <inheritdoc/>

        public virtual long? Id => ClaimsPrincipal.FindUserId<long>();

        /// <inheritdoc />
        public virtual string? UserName => ClaimsPrincipal.FindUserName();

        /// <inheritdoc />
        public virtual string? Email => ClaimsPrincipal.Claims.FirstOrDefault(c => c.Type == CMS.Data.Model.Const.ClaimTypes.Email)?.Value;

        /// <inheritdoc />
        public virtual string[] Roles => FindClaims(CMS.Data.Model.Const.ClaimTypes.Role).Select(c => c.Value.ToString()).ToArray();

        /// <inheritdoc />
        public virtual Guid? TenantId => ClaimsPrincipal.FindTenantId();

        /// <inheritdoc />
        public virtual string? TenantName => ClaimsPrincipal?.Claims?.FirstOrDefault(c => c.Type == CMS.Data.Model.Const.ClaimTypes.TenantName)?.Value;

        /// <inheritdoc />
        public virtual Claim? FindClaim(string claimType)
        {
            return ClaimsPrincipal.Claims.FirstOrDefault(c => c.Type == claimType);
        }

        /// <inheritdoc />
        public virtual Claim[] FindClaims(string claimType)
        {
            return ClaimsPrincipal.Claims.Where(c => c.Type == claimType).ToArray();
        }

        /// <inheritdoc />
        public virtual Claim[] GetAllClaims()
        {
            return ClaimsPrincipal.Claims.ToArray();
        }

        /// <inheritdoc />
        public virtual bool IsInRole(string roleId)
        {
            return FindClaims(CMS.Data.Model.Const.ClaimTypes.Role).Any(c => c.Value == roleId);
        }
    }
}
