using System.Collections.Generic;
using System.Linq;
using CMS.Web.Service.Cms.Admins;
using CMS.Web.Service.Cms.Permissions;
using IGeekFan.FreeKit.Extras.FreeSql;
using LinCms.Entities;

namespace CMS.Web.Service.Cms.Admin;

public class AdminService : ApplicationService, IAdminService
{
	private readonly IAuditBaseRepository<LinPermission> _permissionRepository;
	public AdminService(IAuditBaseRepository<LinPermission> permissionRepository)
	{
		_permissionRepository = permissionRepository;
	}

	public IDictionary<string, List<PermissionDto>> GetAllStructualPermissions()
	{
		return _permissionRepository.Select.ToList()
			.GroupBy(r => r.Module)
			.ToDictionary(
				group => group.Key,
				group =>
					Mapper.Map<List<PermissionDto>>(group.ToList())
			);

	}
}