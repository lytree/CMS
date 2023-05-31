using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.Models;
/// <summary>
/// 草稿
/// </summary>
[Table(Name = "post_context")]
public class PostContext
{
    [Column(Name = "id", IsPrimary = true, IsIdentity = true)]
    public int Id { get; set; }

    [Column(Name = "post_id")]
    public int PostId { get; set; }
    /// <summary>
    /// 内容（markdown格式）
    /// </summary>
    [MaxLength(-1)]
    [Column(Name = "post_context")]
    public string Context { get; set; }


    [Column(Name = "post_version")]
    public DateTime Version { get; set; }
}
