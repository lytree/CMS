using FreeSql.DataAnnotations;

namespace CMS.Data.Models;
[Table(Name = "photo")]
public class Photo: BaseEntity
{
	[Column(IsIdentity = false, IsPrimary = true)]
	public string Id { get; set; }

	/// <summary>
	/// 作品标题
	/// </summary>
	[Column(Name = "photo_title")]
	public string Title { get; set; }

	/// <summary>
	/// 拍摄地点
	/// </summary>
	[Column(Name = "photo_location")]
	public string Location { get; set; }

	/// <summary>
	/// 文件存储位置
	/// </summary>
	[Column(Name = "photo_path")]
	public string FilePath { get; set; }

	/// <summary>
	/// 高度
	/// </summary>
	[Column(Name = "photo_height")]
	public long Height { get; set; }

	/// <summary>
	/// 宽度
	/// </summary>
	[Column(Name = "photo_width")]
	public long Width { get; set; }

	/// <summary>
	/// 创建时间
	/// </summary>
	[Column(Name = "create_time")]
	public DateTime CreateTime { get; set; }
}