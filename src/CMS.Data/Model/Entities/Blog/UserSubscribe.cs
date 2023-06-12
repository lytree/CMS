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
	public class UserSubscribe : BaseEntity<long>, ISoftDelete
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

		/// <summary>
		/// 删除时间
		/// </summary>
		[Column(Position = -2)]
		public virtual DateTime? DeleteTime { get; set; }

		/// <summary>
		/// 是否删除
		/// </summary>
		[Column(Position = -1)]
		public virtual bool IsDeleted { get; set; }


		[Navigate("CreateUserId")]
		public virtual CMSUser LinUser { get; set; }

		[Navigate("SubscribeUserId")]
		public virtual CMSUser SubscribeUser { get; set; }
	}
}
