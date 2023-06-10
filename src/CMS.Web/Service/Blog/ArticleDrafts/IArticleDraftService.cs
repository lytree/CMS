using System;
using System.Threading.Tasks;

namespace CMS.Web.Service.Blog.ArticleDrafts;

public interface IArticleDraftService
{
	Task UpdateAsync(long id, UpdateArticleDraftDto updateArticleDto);

	Task<ArticleDraftDto> GetAsync(long id);
}