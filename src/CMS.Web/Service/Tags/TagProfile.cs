using AutoMapper;
using CMS.Data.Model.Entities.Blog;

namespace CMS.Web.Service.Blog.Tags;

public class TagProfile : Profile
{
	public TagProfile()
	{
		CreateMap<TagEntity, TagListDto>();
		CreateMap<TagEntity, TagDto>();
		CreateMap<CreateUpdateTagDto, TagEntity>();
	}
}