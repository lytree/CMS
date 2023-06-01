using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;

namespace CMS.Data.Models;

/// <summary>
/// 博客文章
/// </summary>
/// 
[Table(Name = "post")]
public class Post : BaseEntity
{
	[Column(IsIdentity = false, IsPrimary = true)]
	public string Id { get; set; }

	/// <summary>
	/// 标题
	/// </summary>
	/// 
	[Column(Name = "post_title")]
	public string Title { get; set; }

	/// <summary>
	/// 文章状态
	/// </summary>
	/// 
	[Column(Name = "post_status")]
	public string? Status { get; set; }

	/// <summary>
	/// 是否发表（不发表的话就是草稿状态）
	/// </summary>
	[Column(Name = "post_public")]
	public bool IsPublish { get; set; } = false;

	/// <summary>
	/// 梗概
	/// </summary>
	[Column(Name = "post_summary")]
	public string? Summary { get; set; }

	/// <summary>
	/// 发布内容（html格式）
	/// </summary>
	[MaxLength(-1)]
	[Column(Name = "html_context")]
	public string? HtmlContext { get; set; }

	/// <summary>
	/// 博客在导入前的相对路径
	/// <para>如："系列/AspNetCore开发笔记"</para>
	/// </summary>
	[Column(Name = "post_title")]
	public string? Path { get; set; }

	public virtual ICollection<PostContext> Contexts { get; set; } = new List<PostContext>();
	public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
	public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
	public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}