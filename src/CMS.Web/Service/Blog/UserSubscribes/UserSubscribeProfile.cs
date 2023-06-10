using AutoMapper;
using LinCms.Entities.Blog;

namespace CMS.Web.Service.Blog.UserSubscribes;

public class UserSubscribeProfile : Profile
{
	public UserSubscribeProfile()
	{
		CreateMap<UserSubscribe, UserSubscribeDto>();
	}
}