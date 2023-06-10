using AutoMapper;
using CMS.Web.Service.Cms.Account;
using CMS.Web.Service.Cms.Admins;
using LinCms.Entities;

namespace CMS.Web.Service.Cms.Users;

public class UserProfile : Profile
{
	public UserProfile()
	{
		CreateMap<CreateUserDto, LinUser>();
		CreateMap<UpdateUserDto, LinUser>();
		CreateMap<LinUser, UserInformation>();
		CreateMap<LinUser, UserDto>();
		CreateMap<LinUser, OpenUserDto>();
		CreateMap<LinUser, UserNoviceDto>();
		CreateMap<RegisterDto, LinUser>();
	}
}