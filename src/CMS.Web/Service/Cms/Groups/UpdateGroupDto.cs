using System.ComponentModel.DataAnnotations;

namespace CMS.Web.Service.Cms.Groups;

public class UpdateGroupDto
{
	[Required(ErrorMessage = "请输入分组名称")]
	public string Name { get; set; }
	public string Info { get; set; }
	public int SortCode { get; set; }
}