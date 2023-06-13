using System;

namespace CMS.Data.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class OrderGuidAttribute : Attribute
{
	public bool Enable { get; set; } = true;
}