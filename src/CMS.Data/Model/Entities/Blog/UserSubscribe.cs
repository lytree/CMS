using System;
using CMS.Data.Model.Entities.Base;
using FreeSql.DataAnnotations;
using CMS.Data.Model.Entities.User;

namespace CMS.Data.Model.Entities.Blog
{
	/// <summary>
	/// 用户关注用户
	/// </summary>
	[Table(Name = "blog_user_subscribe")]
	public class UserSubscribe : BaseEntity<long>
	{
		/// <summary>
		/// 被关注的用户Id
		/// </summary>
		public long SubscribeUserId { get; set; }

		/// <summary>
		/// 关注的用户Id
		/// </summary>
		public long? CreateUserId { get; set; }

		public string CreateUserName { get; set; }

		public DateTime CreateTime { get; set; }

		[Navigate("CreateUserId")]
		public virtual LinUser LinUser { get; set; }

		[Navigate("SubscribeUserId")]
		public virtual LinUser SubscribeUser { get; set; }
	}
}
