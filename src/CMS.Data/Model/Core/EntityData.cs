using FreeSql.DataAnnotations;
using System.ComponentModel;

namespace CMS.Data.Model.Core;

/// <summary>
/// 实体数据权限
/// </summary>
public class EntityData<TKey> : EntityBase, IData
{
    /// <summary>
    /// 拥有者Id
    /// </summary>
    [Description("拥有者Id")]
    [Column(Position = -41)]
    public virtual long? OwnerId { get; set; }
}

/// <summary>
/// 实体数据权限
/// </summary>
public class EntityData : EntityData<long>
{
}