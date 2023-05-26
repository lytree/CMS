using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.Models
{
	public class BaseEntity
	{
		[Column(Name = "create_time")]
		[CreateTime()]
		public DateTime CreateTime { get; set; }
		[LastUpdateTime()]
		[Column(Name = "update_time")]
		public DateTime UpdateTime { get; set; }
	}
}
