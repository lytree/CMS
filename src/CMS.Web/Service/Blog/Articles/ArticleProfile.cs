using AutoMapper;
using CMS.Web.Service.Blog.ArticleDrafts;
using LinCms.Entities.Blog;

namespace CMS.Web.Service.Blog.Articles;

public class ArticleProfile : Profile
{
	public ArticleProfile()
	{
		CreateMap<CreateUpdateArticleDto, Article>();
		CreateMap<Article, ArticleDto>();
		CreateMap<Article, ArticleListDto>();

		CreateMap<UpdateArticleDraftDto, ArticleDraft>();
		CreateMap<ArticleDraft, ArticleDraftDto>();

		CreateMap<Article, ArticleDraft>();
	}
}