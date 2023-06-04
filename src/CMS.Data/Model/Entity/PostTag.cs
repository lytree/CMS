using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Model.Entity
{
	[Table("post_tag")]
	public class PostTag
	{

		[Column("post_id")]
		public int PostId { set; get; }
		[Column("tag_id")]
		public int TagId { set; get; }
		[FreeSql.DataAnnotations.Navigate(nameof(TagId))]
		public Tag Tag { get; set; }
		[FreeSql.DataAnnotations.Navigate(nameof(PostId))]
		public Post Post { get; set; }
	}
}
