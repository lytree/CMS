using CMS.Web.Data;
using System;
using System.Threading.Tasks;


namespace CMS.Web.Service.Blog.Channels;

public interface IChannelService
{
	#region CRUD
	Task<PagedResultDto<ChannelDto>> GetListAsync(ChannelSearchDto searchDto);

	Task<ChannelDto> GetAsync(long id);

	Task CreateAsync(CreateUpdateChannelDto createChannel);

	Task UpdateAsync(long id, CreateUpdateChannelDto updateChannel);
	Task DeleteAsync(long id);
	#endregion

	/// <summary>
	/// 首页减少不必要的字段后，流量字节更少
	/// </summary>
	/// <param name="pageDto"></param>
	/// <returns></returns>
	Task<PagedResultDto<NavChannelListDto>> GetNavListAsync(PageDto pageDto);
}