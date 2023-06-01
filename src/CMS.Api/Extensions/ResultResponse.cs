
using CMS.Api.Extensions;
using CMS.Common;

namespace CMS.Api
{
	public class ResultResponse<T>
	{
		public string Code { get; set; }

		public string Message { get; set; }

		public T? Data { get; set; }

		public ResultResponse(string state, string message, T? data)
		{
			Code = state;
			Message = message;
			Data = data;
		}
		public ResultResponse(string state, string message)
		{
			Code = state;
			Message = message;
		}
		public ResultResponse(ServerCode code)
		{
			Code = code.GetCode();
			Message = code.GetMessage();
		}
		public ResultResponse(ServerCode code, T? data)
		{
			Code = code.GetCode();
			Message = code.GetMessage();
			Data = data;
		}
		public ResultResponse(BusinessCode code)
		{
			Code = code.GetCode();
			Message = code.GetMessage();
		}
		public static ResultResponse<T> Success(T? data)
		{
			return new ResultResponse<T>(ServerCode.Success, data);
		}

		public static ResultResponse<T> Success()
		{
			return new ResultResponse<T>(ServerCode.Success);
		}


		public static ResultResponse<T> Fail()
		{
			return new ResultResponse<T>(ServerCode.FAIL);
		}


		public static ResultResponse<T> Fail(BusinessCode code)
		{
			return new ResultResponse<T>(code);
		}

		public static ResultResponse<T> Fail(ServerCode code)
		{
			return new ResultResponse<T>(code);
		}
	}
}
