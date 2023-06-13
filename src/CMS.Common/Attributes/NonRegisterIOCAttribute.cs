using System;

namespace CMS.Data.Attributes;

/// <summary>
/// 不注册到第三方IOC容器
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class NonRegisterIOCAttribute : Attribute
{
}