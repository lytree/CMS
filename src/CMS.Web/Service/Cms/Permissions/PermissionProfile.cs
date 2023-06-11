using AutoMapper;
using CMS.Data.Model.Entities;

namespace CMS.Web.Service.Cms.Permissions;

public class PermissionProfile : Profile
{
	public PermissionProfile()
	{
		CreateMap<LinPermission, PermissionDto>();
	}
}