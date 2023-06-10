using CMS.Web.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Web.Service.Blog.Classifys;

public interface IClassifyService : ICrudAppService<ClassifyDto, ClassifyDto, long, ClassifySearchDto, CreateUpdateClassifyDto, CreateUpdateClassifyDto>
{
	List<ClassifyDto> GetListByUserId(long? userId);
	Task UpdateArticleCountAsync(long? id, int inCreaseCount);
}