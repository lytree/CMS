using System;
using CMS.Data.Model.Entities.Base;
using FreeSql.DataAnnotations;


namespace CMS.Data.Model.Entities.Blog
{
    /// <summary>
    /// 随笔标签
    /// </summary>
    [Table(Name = "blog_tag_article")]
    public class TagArticle : Entity
    {
        public long TagId { get; set; }

        public long ArticleId { get; set; }

        public virtual Tag Tag { get; set; }

        public virtual Article Article { get; set; }
    }
}
