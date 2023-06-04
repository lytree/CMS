
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
	[Table("config_item")]
	public class ConfigItem : BaseEntity
	{
		[Column("id")]
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Column("item_name", TypeName = "varchar(200)")]
		public string Name { get; set; }

		[Column("item_key", TypeName = "varchar(200)")]
		public string Key { get; set; }

		[Column("item_group_key", TypeName = "varchar(200)")]
		public string GroupKey { get; set; }
		[Column("item_status")]
		public int Status { get; set; }

		[Column("item_context", TypeName = "varchar(200)")]
		public string? Context { get; set; }

		[Column("item_desc", TypeName = "varchar(200)")]
		public string? Description { get; set; }

		public virtual ConfigGroup ConfigGroup { get;  }
	}
}
