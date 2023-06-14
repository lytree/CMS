using System;
using System.Collections.Generic;
using CMS.Data.Model.Entities.Base;
using FreeSql.DataAnnotations;
using CMS.Data.Model.Entities.User;
using CMS.Data.Exceptions;
using CMS.Data.Model.Core;

namespace CMS.Data.Model.Entities.Blog
{
	/// <summary>
	/// 用户评论信息
	/// </summary>
	[Table(Name = "blog_comment")]
	public class CommentEntity : EntityBase
	{
		/// <summary>
		/// 回复评论Id
		/// </summary>
		public long? RespId { get; set; }
		/// <summary>
		/// 根回复id
		/// </summary>
		public long? RootCommentId { get; set; }

		public int ChildsCount { get; set; }

		/// <summary>
		/// 被回复的用户Id
		/// </summary>
		public long? RespUserId { get; set; }
		/// <summary>
		/// 回复的文本内容
		/// </summary>
		[Column(StringLength = 500)]
		public string Text { get; set; }

		/// <summary>
		/// 点赞量
		/// </summary>
		public int LikesQuantity { get; set; }
		/// <summary>
		/// 是否已审核
		/// </summary>
		public bool? IsAudit { get; set; } = true;

		/// <summary>
		/// 关联随笔id
		/// </summary>
		public long? SubjectId { get; set; }

		/// <summary>
		/// 评论的对象 1 是随笔，其他为以后扩展
		/// </summary>
		public int SubjectType { get; set; } = 1;

		/// <summary>
		/// 评论的用户-OneToOne
		/// </summary>
		[Navigate("CreateUserId")]
		public virtual UserEntity UserInfo { get; set; }
		/// <summary>
		/// 被回复的用户-OneToOne
		/// </summary>
		[Navigate("RespUserId")]
		public virtual UserEntity RespUserInfo { get; set; }


		[Navigate("RootCommentId")]
		public virtual ICollection<CommentEntity> Childs { get; set; }



		[Navigate("RespId")]
		public virtual CommentEntity Parent { get; set; }

		public void UpdateLikeQuantity(int likesQuantity)
		{
			if (IsAudit == false)
			{
				throw new AppException("该评论因违规被拉黑");
			}

			if (likesQuantity < 0)
			{
				if (LikesQuantity < -likesQuantity)
				{
					return;
				}
			}

			LikesQuantity += likesQuantity;
		}
	}

}
