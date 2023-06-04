using AutoMapper;
using CMS.Data.Context;
using CMS.Model;
using CMS.Model.DTO;
using CMS.Model.Entity;
using CMS.Model.Param;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service;

public class LinksService
{
	private readonly ILogger<LinksService> _logger;
	private readonly IMapper _mapper;
	private readonly DataContext _dataContext;
	public LinksService(ILoggerFactory loggerFactory, IMapper mapper, DataContext dataContext)
	{
		_logger = loggerFactory == null ? NullLogger<LinksService>.Instance : loggerFactory.CreateLogger<LinksService>();
		_mapper = mapper;
		_dataContext = dataContext;
	}

	public IEnumerable<LinkDto> GetAll()
	{
		var links = _dataContext.Set<Link>();
		return _mapper.Map<List<LinkDto>>(links.AsNoTracking().ToList()).AsEnumerable();
	}

	public void SaveLink(LinkParam param)
	{
		var link = _mapper.Map<LinkParam,Link>(param);
		if (link.Id != null)
		{
			_dataContext.Set<Link>().Update(link);
		}
		else
		{
			_dataContext.Set<Link>().Add(link);
		}
		_dataContext.SaveChanges();
	}
}
