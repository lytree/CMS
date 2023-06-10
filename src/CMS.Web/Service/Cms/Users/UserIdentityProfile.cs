using AutoMapper;
using LinCms.Entities;

namespace CMS.Web.Service.Cms.Users;

public class UserIdentityProfile : Profile
{
	public UserIdentityProfile()
	{
		CreateMap<LinUserIdentity, UserIdentityDto>();

	}
}