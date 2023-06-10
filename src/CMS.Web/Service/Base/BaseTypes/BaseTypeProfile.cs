﻿using AutoMapper;
using CMS.Data.Model.Entities;

namespace CMS.Web.Service.Base.BaseTypes;

public class BaseTypeProfile : Profile
{
	public BaseTypeProfile()
	{
		CreateMap<BaseType, BaseTypeDto>();
		CreateMap<CreateUpdateBaseTypeDto, BaseType>();
	}
}