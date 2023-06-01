using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Common;

public static class EnumHelpExtension
{
	public static string GetMessage(this Enum val)
	{
		var type = val.GetType();

		var memberInfo = type.GetMember(val.ToString());

		var attributes = memberInfo[0].GetCustomAttributes(typeof(MessageAttribute), false);

		if (attributes == null || attributes.Length != 1)
		{
			//如果没有定义描述，就把当前枚举值的对应名称返回
			return val.ToString();
		}

		return ((MessageAttribute)attributes.Single()).Message;
	}
	public static string GetCode(this Enum val)
	{
		var type = val.GetType();

		var memberInfo = type.GetMember(val.ToString());

		var attributes = memberInfo[0].GetCustomAttributes(typeof(CodeAttribute), false);

		if (attributes == null || attributes.Length != 1)
		{
			//如果没有定义描述，就把当前枚举值的对应名称返回
			return "S9999";
		}

		return ((CodeAttribute)attributes.Single()).Code;
	}
}
