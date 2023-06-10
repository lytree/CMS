using System.Collections.Generic;
using CMS.Web.Service.Cms.Permissions;

namespace CMS.Web.Service.Cms.Admins;

public interface IAdminService
{
	IDictionary<string, List<PermissionDto>> GetAllStructualPermissions();
}