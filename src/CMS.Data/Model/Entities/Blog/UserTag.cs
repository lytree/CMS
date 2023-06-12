using System;
using CMS.Data.Model.Entities.Base;
using FreeSql.DataAnnotations;
using CMS.Data.Model.Entities.User;

namespace CMS.Data.Model.Entities.Blog
{
	/// <summary>
	/// 用户关注的标签
	/// </summary>
	[Table(Name = "blog_user_tag")]
    public class UserTag : Entity<long>
    {
        public long TagId { get; set; }

        public long? CreateUserId { get; set; }


        public string CreateUserName { get; set; }


        public DateTime CreateTime { get; set; }

        [Navigate("CreateUserId")]
        public virtual CMSUser LinUser { get; set; }

        [Navigate("TagId")]
        public virtual Tag Tag { get; set; }
    }
}
