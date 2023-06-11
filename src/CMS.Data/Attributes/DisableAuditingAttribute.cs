using System;

namespace CMS.Data.Attributes
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
	public class DisableAuditingAttribute : Attribute
	{

	}
}
