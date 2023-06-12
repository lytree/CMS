using AutoMapper;
using CMS.Data.Model.Entities.User;

namespace CMS.Web.Service.Cms.Groups;

public class GroupProfile : Profile
{
	public GroupProfile()
	{
		CreateMap<CreateGroupDto, CMSGroup>();
		CreateMap<UpdateGroupDto, CMSGroup>();
		CreateMap<CMSGroup, GroupDto>();
	}
}