using AutoMapper;
using LinCms.Entities.Settings;

namespace CMS.Web.Service.Cms.Settings;

public class SettingProfile : Profile
{
	public SettingProfile()
	{
		CreateMap<CreateUpdateSettingDto, LinSetting>();
		CreateMap<LinSetting, SettingDto>();
	}
}