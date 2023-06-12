using CMS.Data.Model.Entities.Base;
using FreeSql.DataAnnotations;


namespace CMS.Data.Model.Entities.User
{
	/// <summary>
	/// 分组权限中间表
	/// </summary>
	[Table(Name = "cms_group_permission")]
	public class CMSGroupPermission : Entity<long>
	{
		public CMSGroupPermission()
		{
		}

		public CMSGroupPermission(long groupId, long permissionId)
		{
			GroupId = groupId;
			PermissionId = permissionId;
		}

		public CMSGroupPermission(long permissionId)
		{
			PermissionId = permissionId;
		}

		/// <summary>
		/// 分组id
		/// </summary>
		public long GroupId { get; set; }

		/// <summary>
		/// 权限Id
		/// </summary>
		public long PermissionId { get; set; }


	}
}
