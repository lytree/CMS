using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Common
{
	public class ServerException : Exception
	{

		public ServerCode ServerCode { get; }

		public ServerException(ServerCode serverCode) : base()
		{
			ServerCode = serverCode;
		}

		public ServerException(ServerCode serverCode, string? message) : base(message)
		{
			ServerCode = serverCode;
		}

		public ServerException(ServerCode serverCode, string? message, Exception? innerException) : base(message, innerException)
		{
			ServerCode = serverCode;
		}
	}
}
