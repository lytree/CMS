
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Common;

public enum ServerCode
{
	[Code("S0000"), Message("请求成功")]
	Success,
	[Code("S9999"), Message("服务器异常")]
	FAIL,

}
