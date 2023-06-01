using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Common;

[AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
internal class CodeAttribute : Attribute
{
	public string Code { get; }

	public CodeAttribute(string Code)
	{
		this.Code = Code;
	}
}

[AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
internal class MessageAttribute : Attribute
{
	public string Message { get; }

	public MessageAttribute(string Message)
	{
		this.Message = Message;
	}
}

