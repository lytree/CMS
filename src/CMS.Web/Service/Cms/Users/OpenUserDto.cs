

namespace CMS.Web.Service.Cms.Users;

public class OpenUserDto 
{
	public OpenUserDto(string nickname)
	{
		Nickname = nickname;
	}

	public OpenUserDto()
	{
	}
	public long Id { get; set; }
	public string Introduction { get; set; }
	public string Username { get; set; }
	public string Nickname { get; set; }
	public string Avatar { get; set; }
	public string BlogAddress { get; set; }
	public string JobTitle { get; set; }
	public string Company { get; set; }

}