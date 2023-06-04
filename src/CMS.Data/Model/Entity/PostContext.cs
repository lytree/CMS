
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Model.Entity;
/// <summary>
/// 草稿
/// </summary>
[Table("post_context")]
public class PostContext
{
	[Column("id")]
	[Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	[Column("post_id")]
	public int PostId { get; set; }
	/// <summary>
	/// 内容（markdown格式）
	/// </summary>
	/// []
	/// 
	[Column("post_context",TypeName = "longtext")]
	public string Context { get; set; }


	[Column("post_version")]
	public DateTime Version { get; set; }


	public virtual Post Post { get; set; }
}
