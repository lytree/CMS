using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.Models
{
	[Table(Name = "comment")]
	public class Comment : BaseEntity
	{

		[Column(Name = "id", IsIdentity = true, IsPrimary = true)]
		public int id { get; set; }

		/**
		 * 此评论的用户名
		 */
		[Column(Name = "comment_username")]
		public string username { get; set; }

		/**
		 * 此评论这的邮箱地址
		 */
		[Column(Name = "comment_email")]
		public string email { get; set; }

		/**
		 * 评论者的ip地址
		 */
		[Column(Name = "comment_ip")]
		public string commentIp { get; set; }
		/**
		 * 此评论是回复哪个评论的
		 */
		[Column(Name = "reply_comment_id")]
		public int replyCommentId { get; set; }

		///**
		// * 如果此评论是回复某条评论，则1：已通知回复的那条评论的邮箱，0：未发送邮箱通知
		// */
		//[Column(Name = "post_id")]
		//public bool emailNotice { get; set; }

		/**
		 * 评论内容
		 */
		[Column(Name = "post_id")]
		public string content { get; set; }

		/**
		 * 1：删除 0：未删除
		 */
		[Column(Name = "post_id")]
		public int status { get; set; }

		/// <summary>
		/// 评论所属id
		/// </summary>
		[Column(Name = "post_id")]
		public int postId { get; set; }
	}
}
