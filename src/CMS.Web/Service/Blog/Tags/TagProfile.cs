using AutoMapper;
using LinCms.Entities.Blog;

namespace CMS.Web.Service.Blog.Tags;

public class TagProfile : Profile
{
	public TagProfile()
	{
		CreateMap<Tag, TagListDto>();
		CreateMap<Tag, TagDto>();
		CreateMap<CreateUpdateTagDto, Tag>();
	}
}