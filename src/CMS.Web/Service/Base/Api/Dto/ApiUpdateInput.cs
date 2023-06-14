using System.ComponentModel.DataAnnotations;

namespace CMS.Web.Service.Base.Api.Dto;

/// <summary>
/// 修改
/// </summary>
public partial class ApiUpdateInput : ApiAddInput
{
	/// <summary>
	/// 接口Id
	/// </summary>
	[Required]
	//[ValidateRequired("请选择接口")]
	public long Id { get; set; }
}