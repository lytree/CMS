using System;

namespace CMS.Web.Aop.Attributes
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
	public class DisableAuditingAttribute : Attribute
	{

	}
}
