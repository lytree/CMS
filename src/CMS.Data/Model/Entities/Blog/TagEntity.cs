﻿using System;
using System.Collections.Generic;
using CMS.Data.Model.Entities.Base;
using FreeSql.DataAnnotations;
using CMS.Data.Model.Entities.User;
using CMS.Data.Model.Core;

namespace CMS.Data.Model.Entities.Blog
{
	/// <summary>
	/// 标签
	/// </summary>
	[Table(Name = "blog_tag")]
	public class TagEntity : EntityBase
	{
		/// <summary>
		/// 别名
		/// </summary>
		[Column(StringLength = 200)]
		public string Alias { get; set; }

		/// <summary>
		/// 标签封面图
		/// </summary>
		[Column(StringLength = 100)]
		public string Thumbnail { get; set; }

		/// <summary>
		/// 标签名
		/// </summary>
		[Column(StringLength = 50)]
		public string TagName { get; set; }

		/// <summary>
		/// 标签状态，true:正常，false：拉黑
		/// </summary>
		public bool Status { get; set; }

		/// <summary>
		/// 随笔数量
		/// </summary>
		public int ArticleCount { get; set; } = 0;

		/// <summary>
		/// 浏览次数
		/// </summary>
		public int ViewHits { get; set; } = 0;

		/// <summary>
		/// 标签备注情况
		/// </summary>
		public string Remark { get; set; }

		/// <summary>
		/// 关注数量
		/// </summary>
		public int SubscribersCount { get; set; } = 0;

		public virtual ICollection<ArticleEntity> Articles { get; set; }

		public virtual ICollection<ChannelEntity> Channels { get; set; }

		public virtual ICollection<ChannelTag> ChannelTags { get; set; }

		[Navigate("CreateUserId")]
		public virtual UserEntity LinUser { get; set; }


		public TagEntity UpdateSubscribersCount(int inCreaseCount)
		{
			//防止数量一直减，减到小于0
			if (inCreaseCount < 0 && SubscribersCount < -inCreaseCount)
			{
				return this;
			}
			SubscribersCount += inCreaseCount;
			return this;
		}

	}
}