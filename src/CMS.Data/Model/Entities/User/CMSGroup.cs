using System;
using System.Collections.Generic;
using CMS.Data.Model.Entities.Base;
using FreeSql.DataAnnotations;


namespace CMS.Data.Model.Entities.User
{
	/// <summary>
	///  分组
	/// </summary>
	[Table(Name = "cms_group")]
	public class CMSGroup : BaseEntity<long>
	{
		public CMSGroup()
		{
		}

		public CMSGroup(string name, string info, bool isStatic)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
			Info = info ?? throw new ArgumentNullException(nameof(info));
			IsStatic = isStatic;
		}

		/// <summary>
		/// 权限组唯一标识字符
		/// </summary>
		[Column(StringLength = 60)]
		public string Name { get; set; }

		/// <summary>
		/// 权限组描述
		/// </summary>
		[Column(StringLength = 100)]
		public string Info { get; set; }

		/// <summary>
		/// 是否是静态分组,是静态时无法删除此分组
		/// </summary>
		public bool IsStatic { get; set; } = false;

		/// <summary>
		/// 排序码，升序
		/// </summary>
		public int SortCode { get; set; }

		[Navigate(ManyToMany = typeof(CMSUserGroup))]
		public virtual ICollection<CMSUser> LinUsers { get; set; }

		/// <summary>
		/// 超级管理员
		/// </summary>
		public const string Admin = "Admin";
		/// <summary>
		/// Cms管理员
		/// </summary>
		public const string CmsAdmin = "CmsAdmin";

		/// <summary>
		/// 普通用户
		/// </summary>
		public const string User = "User";

	}
}
