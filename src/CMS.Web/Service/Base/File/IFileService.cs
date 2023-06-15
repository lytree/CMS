using CMS.Data.Model.Entities.Base;
using CMS.Web.Model.Dto;
using CMS.Web.Service.Base.File.Dto;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMS.Web.Service.Base.File;

/// <summary>
/// 文件接口
/// </summary>
public interface IFileService
{
	Task<PageOutput<FileGetPageOutput>> GetPageAsync(PageInput<FileGetPageDto> input);

	Task DeleteAsync(FileDeleteInput input);

	Task<FileEntity> UploadFileAsync(IFormFile file, string fileDirectory = "", bool fileReName = true);

	Task<List<FileEntity>> UploadFilesAsync([Required] IFormFileCollection files, string fileDirectory = "", bool fileReName = true);
}