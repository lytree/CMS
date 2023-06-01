using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CMS.Data.Models
{
	[Table(Name = "tag")]
	public class Tag : BaseEntity
	{
		[Column(IsIdentity = true, IsPrimary = true)]
		public int Id { get; set; }

		[Column(Name = "tag_name")]
		public string Name { get; set; }
		/// <summary>
		/// 分类是否可见
		/// </summary> 
		[Column(Name = "visible")]
		public bool Visible { get; set; } = true;
		public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
	}
}
