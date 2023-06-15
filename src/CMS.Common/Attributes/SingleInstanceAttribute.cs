﻿using System;

namespace CMS.Common.Attributes;

/// <summary>
/// 单例注入
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
public class SingleInstanceAttribute : Attribute
{
}