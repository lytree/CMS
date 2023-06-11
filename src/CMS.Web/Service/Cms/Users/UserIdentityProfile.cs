using AutoMapper;
using CMS.Data.Model.Entities.User;


namespace CMS.Web.Service.Cms.Users;

public class UserIdentityProfile : Profile
{
	public UserIdentityProfile()
	{
		CreateMap<LinUserIdentity, UserIdentityDto>();

	}
}