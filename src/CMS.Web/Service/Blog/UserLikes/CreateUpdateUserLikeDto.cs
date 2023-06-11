using CMS.Data.Model.Entities.Blog;
using System;

namespace CMS.Web.Service.Blog.UserLikes;

public class CreateUpdateUserLikeDto
{
	public long SubjectId { get; set; }
	/// <summary>
	/// 1.随笔 2 评论
	/// </summary>
	public UserLikeSubjectType SubjectType { get; set; }
}