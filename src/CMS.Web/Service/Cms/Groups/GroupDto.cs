using CMS.Data.Model.Entities.Other;
using System.Collections.Generic;


namespace CMS.Web.Service.Cms.Groups;

public class GroupDto 
{
	public List<LinPermission> Permissions { get; set; }
	public string Name { get; set; }
	public string Info { get; set; }
	public bool IsStatic { get; set; }

}