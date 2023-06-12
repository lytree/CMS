using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.Model.Entities.Base
{
	[Serializable]
	public abstract class BaseEntity<TKey> where TKey : IEquatable<TKey>
	{
		public TKey Id { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateTime { get; set; }
		/// <summary>
		/// 更新时间
		/// </summary>
		public DateTime UpdateTime { get; set; }
	}
}
