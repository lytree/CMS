using AutoMapper;
using CMS.Data.Model.Entities.User;

namespace CMS.Web.Service.Cms.Groups;

public class GroupProfile : Profile
{
	public GroupProfile()
	{
		CreateMap<CreateGroupDto, LinGroup>();
		CreateMap<UpdateGroupDto, LinGroup>();
		CreateMap<LinGroup, GroupDto>();
	}
}