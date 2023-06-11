using CMS.Web.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Web.Service.Blog.Classifys;

public interface IClassifyService 
{
	List<ClassifyDto> GetListByUserId(long? userId);
	Task UpdateArticleCountAsync(long? id, int inCreaseCount);
}