using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Common
{
	public class BusinessException : Exception
	{
		public BusinessCode BusinessCode { get; }

		public BusinessException(BusinessCode businessCode)
		{
			this.BusinessCode = businessCode;
		}

		public BusinessException(BusinessCode businessCode, string? message) : base(message)
		{
			this.BusinessCode = businessCode;
		}

		public BusinessException(BusinessCode businessCode, string? message, Exception? innerException) : base(message, innerException)
		{
			this.BusinessCode = businessCode;
		}
	}
}
