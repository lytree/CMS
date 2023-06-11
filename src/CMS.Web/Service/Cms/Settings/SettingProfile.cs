using AutoMapper;
using CMS.Data.Model.Entities;

namespace CMS.Web.Service.Cms.Settings;

public class SettingProfile : Profile
{
	public SettingProfile()
	{
		CreateMap<CreateUpdateSettingDto, LinSetting>();
		CreateMap<LinSetting, SettingDto>();
	}
}