using CMS.Web.Service.User.Permission.Dto;
using Mapster;
using System.Linq;
using ZhonTai.Admin.Domain.Permission;

namespace CMS.Web.Service.User.Permission;

/// <summary>
/// 映射配置
/// </summary>
public class MapConfig : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config
		.NewConfig<PermissionEntity, PermissionGetDotOutput>()
		.Map(dest => dest.ApiIds, src => src.Apis.Select(a => a.Id));
	}
}