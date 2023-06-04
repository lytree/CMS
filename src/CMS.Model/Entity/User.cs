

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Model.Entity;
[Table("user")]
public class User : BaseEntity
{
	[Column("id")]
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }
	[Column("user_name",TypeName = "varchar(200)")]
	public string Name { get; set; }
	[Column("user_passwd",TypeName = "varchar(200)")]
	public string Password { get; set; }
	[Column("email_name",TypeName = "varchar(200)")]
	public string Email { get; set; }
	[Column("user_phone",TypeName = "varchar(30)")]
	public string Phone { get; set; }
}