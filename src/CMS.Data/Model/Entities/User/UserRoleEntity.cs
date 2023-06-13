
using CMS.Data.Attributes;
using CMS.Data.Model.Core;
using FreeSql.DataAnnotations;


namespace CMS.Data.Model.Entities.User;

/// <summary>
/// 用户角色
/// </summary>
[Table(Name = "ad_user_role")]
[Index("idx_{tablename}_01", nameof(UserId) + "," + nameof(RoleId), true)]
public class UserRoleEntity : EntityAdd
{
	/// <summary>
	/// 用户Id
	/// </summary>
	public long UserId { get; set; }

	[NotGen]
	public UserEntity User { get; set; }

	/// <summary>
	/// 角色Id
	/// </summary>
	public long RoleId { get; set; }

	[NotGen]
	public RoleEntity Role { get; set; }
}