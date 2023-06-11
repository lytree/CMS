using AutoMapper;
using CMS.Data.Model.Entities.Blog;

namespace CMS.Web.Service.Blog.UserSubscribes;

public class UserSubscribeProfile : Profile
{
	public UserSubscribeProfile()
	{
		CreateMap<UserSubscribe, UserSubscribeDto>();
	}
}