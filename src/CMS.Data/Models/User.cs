using FreeSql.DataAnnotations;

namespace CMS.Data.Models;
[Table(Name = "user")]
public class User
{
	[Column(IsIdentity = false, IsPrimary = true)]
	public string Id { get; set; }
	public string Name { get; set; }
	public string Password { get; set; }
}