using System;
using System.Collections.Generic;
using CMS.Web.Service.Cms.Groups;


namespace CMS.Web.Service.Cms.Users;

public class UserDto
{
	public long Id { get; set; }
	public string Username { get; set; }
	public string Nickname { get; set; }
	public string Avatar { get; set; }
	public string Email { get; set; }
	public int Admin { get; set; } = 1;
	public int Active { get; set; }
	public List<GroupDto> Groups { get; set; }
	public DateTime CreateTime { get; set; }
}