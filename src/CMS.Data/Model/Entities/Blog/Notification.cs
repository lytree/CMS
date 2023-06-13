﻿using System;
using CMS.Data.Model.Entities.Base;
using FreeSql.DataAnnotations;
using CMS.Data.Model.Entities.User;
using CMS.Data.Model.Core;

namespace CMS.Data.Model.Entities.Blog
{
	[Table(Name = "blog_notification")]
	public class Notification : EntityBase
	{
		/// <summary>
		/// 消息通知
		/// </summary>
		public NotificationType NotificationType { get; set; }

		/// <summary>
		/// 默认消息未读
		/// </summary>
		public bool IsRead { get; set; } = false;
		/// <summary>
		/// 当前登录人Id，消息接收者
		/// </summary>
		public long NotificationRespUserId { get; set; }

		public long? ArticleId { get; set; }

		public long? CommentId { get; set; }
		/// <summary>
		/// 创建消息者
		/// </summary>
		public long UserInfoId { get; set; }

		/// <summary>
		/// 创建消息者 关联用户Id
		/// </summary>
		[Navigate("UserInfoId")]
		public UserEntity UserInfo { get; set; }

		/// <summary>
		/// 评论消息
		/// </summary>
		[Navigate("CommentId")]
		public Comment CommentEntry { get; set; }

		/// <summary>
		/// 随笔内容
		/// </summary>
		[Navigate("ArticleId")]
		public ArticleEntity ArticleEntry { get; set; }
	}

	public enum NotificationType
	{
		/// <summary>
		/// 用户点赞随笔 
		/// </summary>
		UserLikeArticle = 0,
		/// <summary>
		/// 用户点赞随笔上的评论
		/// </summary>
		UserLikeArticleComment = 1,
		/// <summary>
		/// 用户评论随笔
		/// </summary>
		UserCommentOnArticle = 2,
		/// <summary>
		/// 用户回复随笔的评论
		/// </summary>
		UserCommentOnComment = 3,
		/// <summary>
		/// 用户关注用户
		/// </summary>
		UserLikeUser = 4
	}
}
