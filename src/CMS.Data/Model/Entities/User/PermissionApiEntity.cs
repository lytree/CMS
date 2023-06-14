using CMS.Data.Attributes;
using CMS.Data.Model.Core;
using CMS.Data.Model.Entities.Base;
using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CMS.Data.Model.Entities.User
{
	/// <summary>
	/// 权限接口
	/// </summary>
	[Table(Name = "ad_permission_api")]
	[Index("idx_{tablename}_01", nameof(PermissionId) + "," + nameof(ApiId), true)]
	public class PermissionApiEntity : EntityAdd
	{
		/// <summary>
		/// 权限Id
		/// </summary>
		public long PermissionId { get; set; }

		/// <summary>
		/// 权限
		/// </summary>
		[NotGen]
		public PermissionEntity Permission { get; set; }

		/// <summary>
		/// 接口Id
		/// </summary>
		public long ApiId { get; set; }

		/// <summary>
		/// 接口
		/// </summary>
		[NotGen]
		public ApiEntity Api { get; set; }
	}
}
