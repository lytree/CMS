using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Model.Entity
{
	[Table("post_category")]
	public class PostCategory
	{
		[Column("category_id")]
		public int CategoryId { get; set; }
		[Column("post_id")]
		public int PostId { get; set; }



	}
}
