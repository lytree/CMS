using System;
using CMS.Data.Model.Core;
using CMS.Data.Model.Entities.Base;
using FreeSql.DataAnnotations;


namespace CMS.Data.Model.Entities.Blog
{
	/// <summary>
	/// 随笔标签
	/// </summary>
	[Table(Name = "blog_tag_article")]
    public class TagArticleEntity : EntityAdd
    {
        public long TagId { get; set; }

        public long ArticleId { get; set; }

        public virtual TagEntity Tag { get; set; }

        public virtual ArticleEntity Article { get; set; }
    }
}
