using CMS.Data.Model.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;


namespace CMS.Data.Model.Entities
{
    /// <summary>
    /// 黑名单，实现登录Token的过期
    /// </summary>
    public class BlackRecord : BaseEntity<long>
    {
        /// <summary>
        /// 用户Token
        /// </summary>
        [StringLength(-2)]
        public string Jti { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        [StringLength(50)]
        public string UserName { get; set; }

        /// <inheritdoc />
        public long? CreateUserId { get; set; }

        /// <inheritdoc />
        public string CreateUserName { get; set; }

        /// <inheritdoc />
        public DateTime CreateTime { get; set; }
    }
}
