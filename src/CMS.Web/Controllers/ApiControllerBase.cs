using System;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Controllers;

[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
	public IServiceProvider? ServiceProvider { get; set; }

}