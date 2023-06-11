using System;
using System.Collections.Generic;


namespace CMS.Web.Service.Cms.Permissions;

public class PermissionDto
{
	public PermissionDto(string name, string module, string router)
	{
		Name = name;
		Module = module;
		Router = router;
	}
	public long Id { get; set; }
	public string Name { get; set; }
	public string Module { get; set; }
	public string Router { get; set; }

}

public class TreePermissionDto
{
	public string Rowkey { get; set; }
	public string Name { get; set; }
	public string Router { get; set; }
	public DateTime? CreateTime { get; set; }
	public List<TreePermissionDto> Children { get; set; }

}