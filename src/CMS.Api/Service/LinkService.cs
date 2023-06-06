using AutoMapper;
using CMS.Model;
using CMS.Model.DTO;
using CMS.Model.Entity;
using CMS.Model.Param;
using FreeSql;
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
	private readonly IBaseRepository<Link> _repository;
	public LinksService(ILoggerFactory loggerFactory, IMapper mapper, IBaseRepository<Link> repository)
	{
		_logger = loggerFactory == null ? NullLogger<LinksService>.Instance : loggerFactory.CreateLogger<LinksService>();
		_mapper = mapper;
		_repository = repository;
	}

	public IEnumerable<LinkDto> GetAll()
	{
		var links = _repository.Select.ToList();
		return _mapper.Map<List<LinkDto>>(links).AsEnumerable();
	}

	public void SaveLink(LinkParam param)
	{
		var link = _mapper.Map<LinkParam, Link>(param);
		_repository.InsertOrUpdate(link);
	}
}
