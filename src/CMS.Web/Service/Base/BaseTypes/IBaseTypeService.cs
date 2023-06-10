﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Web.Service.Base.BaseTypes;

public interface IBaseTypeService
{
	Task DeleteAsync(long id);

	Task<List<BaseTypeDto>> GetListAsync();

	Task<BaseTypeDto> GetAsync(int id);

	Task CreateAsync(CreateUpdateBaseTypeDto createBaseType);

	Task UpdateAsync(int id, CreateUpdateBaseTypeDto updateBaseType);
}