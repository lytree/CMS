﻿using AutoMapper;
using LinCms.Entities.Blog;

namespace CMS.Web.Service.Blog.UserLikes;

public class UserLikeProfile : Profile
{
	public UserLikeProfile()
	{
		CreateMap<CreateUpdateUserLikeDto, UserLike>();
	}
}