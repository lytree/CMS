using System.Collections.Generic;
using System.Linq;
using CMS.Data.Model.Entities;
using CMS.Data.Repository;
using CMS.Web.Service.Cms.Admins;
using CMS.Web.Service.Cms.Permissions;

namespace CMS.Web.Service.Cms.Admin;

public class AdminService : ApplicationService, IAdminService
{
	private readonly IAuditBaseRepository<LinPermission,long> _permissionRepository;
	public AdminService(IAuditBaseRepository<LinPermission,long> permissionRepository)
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