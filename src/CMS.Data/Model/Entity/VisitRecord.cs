

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Model.Entity;

/// <summary>
/// 访问记录
/// </summary>
[Table("visit_record")]
public class VisitRecord
{
	[Column("id")]
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	/// <summary>
	/// IP地址
	/// </summary>
	/// 
	[Column("ip",TypeName = "varchar(50)")]
	public string? Ip { get; set; }

	/// <summary>
	/// 请求路径
	/// </summary>
	[Column("request_path",TypeName = "varchar(200)")]
	public string RequestPath { get; set; }

	/// <summary>
	/// 请求参数
	/// </summary>
	[Column("request_query_string",TypeName = "varchar(200)")]
	public string? RequestQueryString { get; set; }

	/// <summary>
	/// 请求方法
	/// </summary>
	[Column("request_method",TypeName = "varchar(200)")]
	public string RequestMethod { get; set; }

	/// <summary>
	/// 用户设备
	/// </summary>
	[Column("user_agent",TypeName = "varchar(200)")]
	public string UserAgent { get; set; }

	/// <summary>
	/// 时间
	/// </summary>
	[Column("request_time")]
	public DateTime Time { get; set; }
}