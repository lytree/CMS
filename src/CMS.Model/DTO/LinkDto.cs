using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CMS.Model.DTO;

public class LinkDto
{

    public int? Id { get; set; }

    /// <summary>
    /// 网站名称
    /// </summary>

    public string Name { get; set; }

    /// <summary>
    /// 介绍
    /// </summary>

    public string? Description { get; set; }

    /// <summary>
    /// 网址
    /// </summary>

    public string Url { get; set; }

    /// <summary>
    /// 是否显示
    /// </summary>
    public bool Visible { get; set; }
}
