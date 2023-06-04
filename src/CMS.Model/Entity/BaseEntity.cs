
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Model.Entity
{
	public class BaseEntity
	{
		[Column("create_time")]
		public DateTime? CreateTime { get; set; }
		[Column("update_time")]
		public DateTime? UpdateTime { get; set; }
	}
}
