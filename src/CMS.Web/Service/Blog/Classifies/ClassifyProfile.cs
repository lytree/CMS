using AutoMapper;
using CMS.Data.Model.Entities.Blog;
using CMS.Web.Service.Blog.Classifys;


namespace CMS.Web.Service.Blog.Classifies;

public class ClassifyProfile : Profile
{
	public ClassifyProfile()
	{
		CreateMap<CreateUpdateClassifyDto, Classify>();
		CreateMap<Classify, ClassifyDto>();
	}
}