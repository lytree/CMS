using AutoMapper;
using CMS.Data.Model.Entities.User;
using CMS.Web.Service.Cms.Account;
using CMS.Web.Service.Cms.Admins;


namespace CMS.Web.Service.Cms.Users;

public class UserProfile : Profile
{
	public UserProfile()
	{
		CreateMap<CreateUserDto, CMSUser>();
		CreateMap<UpdateUserDto, CMSUser>();
		CreateMap<CMSUser, UserInformation>();
		CreateMap<CMSUser, UserDto>();
		CreateMap<CMSUser, OpenUserDto>();
		CreateMap<CMSUser, UserNoviceDto>();
		CreateMap<RegisterDto, CMSUser>();
	}
}