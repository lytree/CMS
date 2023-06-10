using System;
using System.Threading.Tasks;

namespace CMS.Web.Service.Blog.Tags;

public interface IUserTagService
{
	/// <summary>
	/// 用户关注标签
	/// </summary>
	/// <param name="tagId"></param>
	Task CreateUserTagAsync(long tagId);

	/// <summary>
	/// 当前用户取消关注标签
	/// </summary>
	/// <param name="tagId"></param>
	/// <returns></returns>
	Task DeleteUserTagAsync(long tagId);
}