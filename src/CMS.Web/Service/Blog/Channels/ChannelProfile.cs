using AutoMapper;
using CMS.Data.Model.Entities.Blog;

namespace CMS.Web.Service.Blog.Channels;

public class ChannelProfile : Profile
{
	public ChannelProfile()
	{
		CreateMap<CreateUpdateChannelDto, Channel>();
		CreateMap<Channel, ChannelDto>();
		CreateMap<Channel, NavChannelListDto>();
	}
}