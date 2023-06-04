
using AutoMapper;
using CMS.Model.DTO;
using CMS.Model.Param;
using CMS.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.OpenApi.Any;
using Swashbuckle.AspNetCore.Annotations;

namespace CMS.Api.Controllers;

[Route("api/links")]
[ApiController]
public class LinksController : ControllerBase
{
	private readonly LinksService _linksService;
	private readonly ILogger<LinksController> _logger;
	private readonly IMapper _mapper;

	public LinksController(LinksService linksService, ILoggerFactory loggerFactory, IMapper mapper)
	{
		_linksService = linksService;
		_logger = loggerFactory != null ? loggerFactory.CreateLogger<LinksController>() : NullLogger<LinksController>.Instance;
		_mapper = mapper;
	}
	[HttpGet(Name = "/")]
	[SwaggerResponse(200, "The product was created", typeof(ResultResponse<IEnumerable<LinkDto>>))]
	public IEnumerable<LinkDto> GetAllLinks()
	{
		return _linksService.GetAll();
	}

	[HttpPost(Name = "/")]
	[SwaggerResponse(200, "The product was created", typeof(ResultResponse<AnyType>))]
	public void SaveLink([FromBody] LinkParam link)
	{

		_linksService.SaveLink(link );
	}
}
