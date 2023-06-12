using CMS.Web.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Web.Service.Blog.Classifies;

public interface IClassifyService
{
    List<ClassifyDto> GetListByUserId(long? userId);


    Task UpdateArticleCountAsync(long? id, int inCreaseCount);

    Task<PagedResultDto<ClassifyDto>> GetListAsync(ClassifySearchDto input);

    Task<ClassifyDto> GetAsync(long id);

    Task<ClassifyDto> CreateAsync(CreateUpdateClassifyDto createInput);

    Task<ClassifyDto> UpdateAsync(long id, CreateUpdateClassifyDto updateInput);

    Task DeleteAsync(long id);
}