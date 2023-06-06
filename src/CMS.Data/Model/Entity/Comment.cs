
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Model.Entity
{
	[Table("comment")]
	public class Comment : BaseEntity
	{

		[Column("id")]
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int? Id { get; set; }

		/**
		 * 此评论的用户名
		 */
		[Column("comment_username", TypeName = "varchar(200)")]
		public string Username { get; set; }

		/**
		 * 此评论这的邮箱地址
		 */
		[Column("comment_email", TypeName = "varchar(200)")]
		public string? Email { get; set; }

		/**
		 * 评论者的ip地址
		 */
		[Column("comment_ip", TypeName = "varchar(200)")]
		public string? CommentIp { get; set; }
		/**
		 * 此评论是回复哪个评论的
		 */
		[Column("reply_comment_id")]
		public int ReplyCommentId { get; set; }

		///**
		// * 如果此评论是回复某条评论，则1：已通知回复的那条评论的邮箱，0：未发送邮箱通知
		// */
		//[Column( "post_id")]
		//public bool emailNotice { get; set; }

		/**
		 * 评论内容
		 */
		[Column("comment_content", TypeName = "varchar(1024)")]
		public string Content { get; set; }

		/**
		 * 1：删除 0：未删除
		 */
		[Column("comment_status")]
		public int Status { get; set; }

		/// <summary>
		/// 评论所属id
		/// </summary>
		[Column("post_id")]
		public int PostId { get; set; }

		[FreeSql.DataAnnotations.Navigate(nameof(PostId))]
		public virtual Post Post { get; set; }
	}
}
