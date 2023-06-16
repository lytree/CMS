using CMS.Web.Attributes;
using System.ComponentModel.DataAnnotations;


namespace CMS.Web.Service.Base.File.Dto;

public class FileDeleteInput
{
	/// <summary>
	/// 文件Id
	/// </summary>
	[Required]
	[ValidateRequired("请选择文件")]
	public long Id { get; set; }
}