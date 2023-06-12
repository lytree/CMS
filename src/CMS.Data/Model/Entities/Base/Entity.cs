using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.Model.Entities.Base
{
	public class Entity<TKey> where TKey : IEquatable<TKey>
	{
		public TKey Id { get; set; }
	}
}
