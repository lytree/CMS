using System;
using LinCms.Data;

namespace CMS.Web.Service.Blog.Comments;

public class CommentSearchDto : PageDto
{
	public long? RootCommentId { get; set; }
	public long? SubjectId { get; set; }

	public string Text { get; set; }
}