using AutoMapper;
using CMS.Data.Model.Entities.User;
using CMS.Web.Service.Cms.Account;
using CMS.Web.Service.Cms.Admins;


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