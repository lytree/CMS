using CMS.Data.Model.Enums;
using System;

namespace CMS.Data.Exceptions
{
	[Serializable]
	public class CMSException : ApplicationException
	{
		/// <summary>
		/// 状态码
		/// </summary>
		private readonly int _code;
		/// <summary>
		/// 错误码，当为0时，代表正常
		/// </summary>

		private readonly ErrorCode _errorCode;
		/// <summary>
		///
		/// </summary>
		public CMSException() : base("服务器繁忙，请稍后再试!")
		{
			_errorCode = ErrorCode.Fail;
			_code = 400;
		}

		public CMSException(string message = "服务器繁忙，请稍后再试!", ErrorCode errorCode = ErrorCode.Fail, int code = 400) : base(message)
		{
			_errorCode = errorCode;
			_code = code;

		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public int GetCode()
		{
			return _code;
		}

		public ErrorCode GetErrorCode()
		{
			return _errorCode;
		}

		protected CMSException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
		{
			throw new NotImplementedException();
		}

		public CMSException(string? message) : base(message)
		{
		}

		public CMSException(string? message, Exception? innerException) : base(message, innerException)
		{
		}
	}
}
