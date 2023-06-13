﻿namespace CMS.Data.Model.Core;

/// <summary>
/// 实体基类
/// </summary>
public class EntityBase<TKey> : EntityDelete<TKey> where TKey : struct
{
}

/// <summary>
/// 实体基类
/// </summary>
public class EntityBase : EntityBase<long>
{
}