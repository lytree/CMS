using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Model.Entity;

/// <summary>
/// 博客文章
/// </summary>
/// 
[Table("post")]
public class Post : BaseEntity
{
	[Column("id")]
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int? Id { get; set; }

	/// <summary>
	/// 标题
	/// </summary>
	/// 
	[Column("post_title")]
	public string Title { get; set; }

	/// <summary>
	/// 文章状态
	/// </summary>
	/// 
	[Column("post_status")]
	public string? Status { get; set; }

	/// <summary>
	/// 是否发表（不发表的话就是草稿状态）
	/// </summary>
	[Column("post_public")]
	public bool IsPublish { get; set; } = false;

	/// <summary>
	/// 梗概
	/// </summary>
	[Column("post_summary", TypeName = "varchar(1024)")]
	public string? Summary { get; set; }

	/// <summary>
	/// 发布内容（html格式）
	/// </summary>
	[MaxLength(-1)]
	[Column("html_context", TypeName = "longtext")]
	public string? HtmlContext { get; set; }

	/// <summary>
	/// 博客在导入前的相对路径
	/// <para>如："系列/AspNetCore开发笔记"</para>
	/// </summary>
	[Column("post_path")]
	public string? Path { get; set; }
	[FreeSql.DataAnnotations.Navigate(nameof(PostContext.PostId))]
	public virtual IEnumerable<PostContext> Contexts { get; set; } = new List<PostContext>();
	public virtual IEnumerable<Category> Categories { get; set; } = new List<Category>();
	public virtual IEnumerable<Tag> Tags { get; set; } = new List<Tag>();
	[FreeSql.DataAnnotations.Navigate(nameof(Comment.PostId))]
	public virtual IEnumerable<Comment> Comments { get; set; } = new List<Comment>();
}