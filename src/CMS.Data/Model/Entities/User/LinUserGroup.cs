using CMS.Data.Model.Entities.Base;
using FreeSql.DataAnnotations;


namespace CMS.Data.Model.Entities.User
{
	/// <summary>
	/// 用户分组中间表
	/// </summary>
	[Table(Name = "lin_user_group")]
	public class LinUserGroup : Entity
	{
		public LinUserGroup()
		{
		}
		public LinUserGroup(long userId, long groupId)
		{
			UserId = userId;
			GroupId = groupId;
		}

		public long UserId { get; set; }

		public long GroupId { get; set; }

		[Navigate("UserId")]
		public LinUser LinUser { get; set; }

		[Navigate("GroupId")]
		public LinGroup LinGroup { get; set; }
	}
}
