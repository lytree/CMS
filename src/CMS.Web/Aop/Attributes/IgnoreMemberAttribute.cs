using System;

namespace CMS.Web.Aop.Attributes
{
	// source: https://github.com/jhewlett/ValueObject
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class IgnoreMemberAttribute : Attribute
	{
	}
}
