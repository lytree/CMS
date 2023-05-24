using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;

namespace CMS.Data.Models;

/// <summary>
/// 博客文章
/// </summary>
/// 
[Table(Name = "post")]
public class Post
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
	/// 文章状态，提取原markdown文件的文件名前缀，用于区分文章状态，例子如下
	/// </summary>
	/// 
	[Column(Name = "post_title")]
	public string? Status { get; set; }

	/// <summary>
	/// 是否发表（不发表的话就是草稿状态）
	/// </summary>
	[Column(Name = "post_title")]public bool IsPublish { get; set; }

	/// <summary>
	/// 梗概
	/// </summary>
	[Column(Name = "post_title")]public string? Summary { get; set; }

	/// <summary>
	/// 内容（markdown格式）
	/// </summary>
	[MaxLength(-1)]
	[Column(Name = "post_title")]public string? Content { get; set; }

	/// <summary>
	/// 博客在导入前的相对路径
	/// <para>如："系列/AspNetCore开发笔记"</para>
	/// </summary>
	[Column(Name = "post_title")]public string? Path { get; set; }

	/// <summary>
	/// 创建时间
	/// </summary>
	[Column(Name = "post_title")]public DateTime CreationTime { get; set; }

	/// <summary>
	/// 上次更新时间
	/// </summary>
	[Column(Name = "post_title")]public DateTime LastUpdateTime { get; set; }

	/// <summary>
	/// 分类ID
	/// </summary>
	[Column(Name = "post_title")]public int CategoryId { get; set; }

	/// <summary>
	/// 分类
	/// </summary>
	public Category? Category { get; set; }

	public string? Categories { get; set; }
}