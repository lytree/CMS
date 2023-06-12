using CMS.Web.Aop.User;
using System.Security.Claims;

namespace CMS.Web.Extensions
{
	public static class CurrentUserExtensions
	{
		#region 获取UserId
		/// <summary>
		/// 获取泛型用户Id
		/// </summary>
		/// <param name="currentUser"></param>
		/// <returns></returns>
		public static long? FindUserId(this CurrentUser currentUser)
		{
			if (currentUser.Id == null) return default;
			return currentUser.Id;
		}
		#endregion

		/// <summary>
		///  姓名
		/// </summary>
		/// <param name="currentUser"></param>
		/// <returns></returns>
		public static string? FindName(this CurrentUser currentUser)
		{
			Claim? claim = currentUser.FindClaim(CMS.Data.Model.Const.ClaimTypes.Name);
			return claim?.Value;
		}

		/// <summary>
		///  手机号
		/// </summary>
		/// <param name="currentUser"></param>
		/// <returns></returns>
		public static string? FindPhoneNumber(this CurrentUser currentUser)
		{
			Claim? claim = currentUser.FindClaim(CMS.Data.Model.Const.ClaimTypes.PhoneNumber);
			return claim?.Value;
		}
	}
}

