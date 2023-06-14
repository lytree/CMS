using System.Collections.Generic;

namespace CMS.Web.Service.Base.Api.Dto;

/// <summary>
/// 接口同步
/// </summary>
public class ApiSyncInput
{
	public List<ApiSyncDto> Apis { get; set; }
}