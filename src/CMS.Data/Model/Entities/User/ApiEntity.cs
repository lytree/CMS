﻿using CMS.Data.Attributes;
using CMS.Data.Model.Core;
using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.Model.Entities.User
{

	/// <summary>
	/// 接口管理
	/// </summary>
	[Table(Name = "ad_api")]
	[Index("idx_{tablename}_01", nameof(ParentId) + "," + nameof(Path), true)]
	public partial class ApiEntity : EntityBase
	{
		/// <summary>
		/// 所属模块
		/// </summary>
		public long ParentId { get; set; }

		/// <summary>
		/// 接口命名
		/// </summary>
		[Column(StringLength = 50)]
		public string Name { get; set; }

		/// <summary>
		/// 接口名称
		/// </summary>
		[Column(StringLength = 500)]
		public string Label { get; set; }

		/// <summary>
		/// 接口地址
		/// </summary>
		[Column(StringLength = 500)]
		public string Path { get; set; }

		/// <summary>
		/// 接口提交方法
		/// </summary>
		[Column(StringLength = 50)]
		public string HttpMethods { get; set; }

		/// <summary>
		/// 说明
		/// </summary>
		[Column(StringLength = 500)]
		public string Description { get; set; }

		/// <summary>
		/// 排序
		/// </summary>
		public int Sort { get; set; }

		/// <summary>
		/// 启用
		/// </summary>
		public bool Enabled { get; set; } = true;

		[Navigate(nameof(ParentId))]
		public List<ApiEntity> Childs { get; set; }

		[NotGen]
		[Navigate(ManyToMany = typeof(PermissionApiEntity))]
		public ICollection<PermissionEntity> Permissions { get; set; }
	}
}
