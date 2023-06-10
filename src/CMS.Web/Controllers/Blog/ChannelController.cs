using System;
using System.Threading.Tasks;
using CMS.Web.Aop.Filter;
using CMS.Web.Data;
using CMS.Web.Service.Blog.Channels;


using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Controllers.Blog;

/// <summary>
/// 技术频道
/// </summary>
[ApiExplorerSettings(GroupName = "blog")]
[Area("blog")]
[Route("api/blog/channels")]
[ApiController]
public class ChannelController : ControllerBase
{
	private readonly IChannelService _channelService;
	public ChannelController(IChannelService channelService)
	{
		_channelService = channelService;
	}

	[CMSAuthorize("删除技术频道", "技术频道")]
	[HttpDelete("{id}")]
	public async Task<UnifyResponseDto> DeleteAsync(long id)
	{
		await _channelService.DeleteAsync(id);
		return UnifyResponseDto.Success();
	}

	[CMSAuthorize("技术频道列表", "技术频道")]
	[HttpGet]
	public Task<PagedResultDto<ChannelDto>> GetListAsync([FromQuery] ChannelSearchDto searchDto)
	{
		return _channelService.GetListAsync(searchDto);
	}

	/// <summary>
	/// 首页显示频道及对应的标签列
	/// </summary>
	/// <param name="pageDto"></param>
	/// <returns></returns>
	[HttpGet("nav")]
	public async Task<PagedResultDto<NavChannelListDto>> GetNavListAsync([FromQuery] PageDto pageDto)
	{
		return await _channelService.GetNavListAsync(pageDto);
	}

	[HttpGet("{id}")]
	public Task<ChannelDto> GetAsync(long id)
	{
		return _channelService.GetAsync(id);
	}

	[CMSAuthorize("新增技术频道", "技术频道")]
	[HttpPost]
	public async Task<UnifyResponseDto> CreateAsync([FromBody] CreateUpdateChannelDto createChannel)
	{
		await _channelService.CreateAsync(createChannel);
		return UnifyResponseDto.Success("新建技术频道成功");
	}

	[CMSAuthorize("修改技术频道", "技术频道")]
	[HttpPut("{id}")]
	public async Task<UnifyResponseDto> UpdateAsync(long id, [FromBody] CreateUpdateChannelDto updateChannel)
	{
		await _channelService.UpdateAsync(id, updateChannel);
		return UnifyResponseDto.Success("更新技术频道成功");
	}
}