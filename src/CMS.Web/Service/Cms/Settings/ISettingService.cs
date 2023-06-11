﻿using CMS.Web.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace CMS.Web.Service.Cms.Settings;

public interface ISettingService
{
	Task<PagedResultDto<SettingDto>> GetPagedListAsync(PageDto pageDto);

	Task<SettingDto> GetAsync(long id);

	Task Delete(string name, string providerName, string providerKey);

	Task<List<SettingDto>> GetListAsync(string providerName, string providerKey);

	Task<string> GetOrNullAsync(string name, string providerName, string providerKey);

	/// <summary>
	/// 用户设置自己的一些配置
	/// </summary>
	/// <param name="createSetting"></param>
	/// <returns></returns>
	Task SetAsync(CreateUpdateSettingDto createSetting);

	Task CreateAsync(CreateUpdateSettingDto createSettingDto);

	Task UpdateAsync(long id, CreateUpdateSettingDto updateSettingDto);
}