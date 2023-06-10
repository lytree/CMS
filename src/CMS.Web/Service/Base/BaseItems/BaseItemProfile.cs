using AutoMapper;
using CMS.Data.Model.Entities;

namespace CMS.Web.Service.Base.BaseItems;

public class BaseItemProfile : Profile
{
	public BaseItemProfile()
	{
		CreateMap<BaseItem, BaseItemDto>();
		CreateMap<CreateUpdateBaseItemDto, BaseItem>();
	}
}