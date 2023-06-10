using System;
using System.Threading.Tasks;
using CMS.Web.Data;
using CMS.Web.Service.Blog.Notifications;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Controllers.Blog;

/// <summary>
/// 消息通知
/// </summary>
[ApiExplorerSettings(GroupName = "blog")]
[Area("blog")]
[Route("api/blog/notifications")]
[ApiController]
public class NotificationController : ControllerBase
{
	private readonly INotificationService _notificationService;

	public NotificationController(INotificationService notificationService)
	{
		_notificationService = notificationService;
	}

	[HttpGet]
	public async Task<PagedResultDto<NotificationDto>> GetListAsync([FromQuery] NotificationSearchDto pageDto)
	{
		return await _notificationService.GetListAsync(pageDto);
	}

	[NonAction]
	[CapSubscribe(CreateNotificationDto.CreateOrCancelAsync)]
	public async Task<UnifyResponseDto> CreateOrCancelAsync([FromBody] CreateNotificationDto createNotification)
	{
		await _notificationService.CreateOrCancelAsync(createNotification);
		return UnifyResponseDto.Success("新建消息成功");
	}

	[HttpPut("{id}")]
	public async Task SetNotificationReadAsync(long id)
	{
		await _notificationService.SetNotificationReadAsync(id);
	}
}