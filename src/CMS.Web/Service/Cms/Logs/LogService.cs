﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CMS.Data.Model.Entities;
using CMS.Data.Repository;
using CMS.Web.Data;
using CMS.Web.Service;


namespace CMS.Web.Service.Cms.Logs;

public class LogService : ApplicationService, ILogService
{
	private readonly IAuditBaseRepository<LinLog, long> _linLogRepository;
	private readonly IUserRepository _linUserAuditBaseRepository;
	public LogService(IAuditBaseRepository<LinLog, long> linLogRepository, IUserRepository linUserAuditBaseRepository)
	{
		_linLogRepository = linLogRepository;
		_linUserAuditBaseRepository = linUserAuditBaseRepository;
	}

	public async Task CreateAsync(LinLog linlog)
	{
		linlog.CreateTime = DateTime.Now;
		linlog.Username = CurrentUser.UserName;
		linlog.UserId = CurrentUser.FindUserId() ?? 0;

		await _linLogRepository.InsertAsync(linlog);
	}

	public PagedResultDto<LinLog> GetUserLogs(LogSearchDto searchDto)
	{
		List<LinLog> linLogs = _linLogRepository.Select
			.WhereIf(!string.IsNullOrEmpty(searchDto.Keyword), r => r.Message.Contains(searchDto.Keyword))
			.WhereIf(!string.IsNullOrEmpty(searchDto.Name), r => r.Username.Contains(searchDto.Name))
			.WhereIf(searchDto.Start.HasValue, r => r.CreateTime >= searchDto.Start.Value)
			.WhereIf(searchDto.End.HasValue, r => r.CreateTime <= searchDto.End.Value)
			.OrderByDescending(r => r.Id)
			.ToPagerList(searchDto, out long totalCount);

		return new PagedResultDto<LinLog>(linLogs, totalCount);

	}

	public List<string> GetLoggedUsers(PageDto searchDto)
	{
		List<string> linLogs = _linLogRepository.Select
			.Where(r => !string.IsNullOrEmpty(r.Username))
			.Distinct()
			.ToList(r => r.Username);

		return linLogs;
	}


	public VisitLogUserDto GetUserAndVisits()
	{
		DateTime now = DateTime.Now;
		DateTime lastMonth = DateTime.Now.AddMonths(-1);

		long totalVisitsCount = _linLogRepository.Select.Count();
		long totalUserCount = _linUserAuditBaseRepository.Select.Count();
		long monthVisitsCount = _linLogRepository.Select.Where(r => r.CreateTime >= lastMonth && r.CreateTime <= now).Count();
		long monthUserCount = _linUserAuditBaseRepository.Select.Where(r => r.CreateTime >= lastMonth && r.CreateTime <= now).Count();

		return new VisitLogUserDto()
		{
			TotalVisitsCount = totalVisitsCount,
			TotalUserCount = totalUserCount,
			MonthVisitsCount = monthVisitsCount,
			MonthUserCount = monthUserCount
		};
	}
}