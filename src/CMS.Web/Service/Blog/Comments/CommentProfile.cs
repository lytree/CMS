using AutoMapper;
using CMS.Data.Model.Entities;

namespace CMS.Web.Service.Blog.Comments;

public class CommentProfile : Profile
{
	public CommentProfile()
	{
		CreateMap<CreateCommentDto, Comment>();
		CreateMap<Comment, CommentDto>().ForMember(d => d.TopComment, opts => opts.Ignore());
	}
}