using FreeSql.DataAnnotations;

namespace CMS.Data.Models;
[Table(Name = "user")]
public class User : BaseEntity
{
	[Column(IsIdentity = true, IsPrimary = true)]
	public int Id { get; set; }
	[Column(Name = "user_name")]
	public string Name { get; set; }
	[Column(Name = "user_passwd")]
	public string Password { get; set; }
	[Column(Name = "email_name")]
	public string Email { get; set; }
	[Column(Name = "user_phone")]
	public string Phone { get; set; }
}