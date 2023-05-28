using FreeSql.DataAnnotations;

namespace CMS.Data.Models;
[Table(Name = "user")]
public class User : BaseEntity
{
	[Column(IsIdentity = true, IsPrimary = true)]
	public int Id { get; set; }
	public string Name { get; set; }
	public string Password { get; set; }
}