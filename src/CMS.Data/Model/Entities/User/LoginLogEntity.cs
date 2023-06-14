using CMS.Data.Model.Entities.Base;
using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CMS.Data.Model.Entities.User
{
	/// <summary>
	/// 登录日志
	/// </summary>
	[Table(Name = "ad_login_log")]
	public partial class LoginLogEntity : LogAbstract
	{
	}
}
