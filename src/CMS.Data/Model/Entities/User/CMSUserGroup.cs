using CMS.Data.Model.Entities.Base;
using FreeSql.DataAnnotations;


namespace CMS.Data.Model.Entities.User
{
	/// <summary>
	/// 用户分组中间表
	/// </summary>
	[Table(Name = "cms_user_group")]
	public class CMSUserGroup : Entity<long>
	{
		public CMSUserGroup()
		{
		}
		public CMSUserGroup(long userId, long groupId)
		{
			UserId = userId;
			GroupId = groupId;
		}

		public long UserId { get; set; }

		public long GroupId { get; set; }

		[Navigate("UserId")]
		public CMSUser LinUser { get; set; }

		[Navigate("GroupId")]
		public CMSGroup LinGroup { get; set; }
	}
}
