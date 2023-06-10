using System.Collections.Generic;
using LinCms.Data.Enums;

namespace CMS.Web.Service.Cms.Admins;

public class UpdateUserDto
{
	public string Email { get; set; }
	public string Nickname { get; set; }
	public string Username { get; set; }
	public UserStatus Active { get; set; }
	public List<long> GroupIds { get; set; }
}