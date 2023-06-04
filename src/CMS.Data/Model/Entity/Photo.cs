

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Model.Entity;
[Table("photo")]
public class Photo : BaseEntity
{
	[Column("id")]
	[Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public string Id { get; set; }

	/// <summary>
	/// 作品标题
	/// </summary>
	[Column("photo_title",TypeName = "varchar(200)")]
	public string Title { get; set; }

	/// <summary>
	/// 拍摄地点
	/// </summary>
	[Column("photo_location",TypeName = "varchar(200)")]
	public string Location { get; set; }

	/// <summary>
	/// 文件存储位置
	/// </summary>
	[Column("photo_path",TypeName = "varchar(200)")]
	public string FilePath { get; set; }

	/// <summary>
	/// 高度
	/// </summary>
	[Column("photo_height")]
	public long Height { get; set; }

	/// <summary>
	/// 宽度
	/// </summary>
	[Column("photo_width")]
	public long Width { get; set; }

}