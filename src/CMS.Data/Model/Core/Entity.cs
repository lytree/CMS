using CMS.Data.Attributes;
using FreeSql.DataAnnotations;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace CMS.Data.Model.Core;

public interface IEntity<TKey>
{
	/// <summary>
	/// 主键Id
	/// </summary>
	TKey Id { get; set; }
}

public interface IEntity : IEntity<long>
{
}

public class Entity<TKey> : IEntity<TKey>
{
	/// <summary>
	/// 主键Id
	/// </summary>
	[Description("主键Id")]
	[Snowflake]
	[Column(Position = 1, IsIdentity = false, IsPrimary = true)]
	[JsonProperty(Order = -30)]
	[JsonPropertyOrder(-30)]
	public virtual TKey Id { get; set; }
}

public class Entity : Entity<long>
{
}