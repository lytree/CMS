
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CMS.Model.Entity
{
	[Table("tag")]
	public class Tag : BaseEntity
	{
		[Column("id")]
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Column("tag_name",TypeName = "varchar(200)")]
		public string Name { get; set; }
		/// <summary>
		/// 分类是否可见
		/// </summary> 
		[Column("visible")]
		public bool Visible { get; set; } = true;


		public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
	}
}
