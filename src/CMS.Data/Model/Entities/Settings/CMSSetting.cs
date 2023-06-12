using CMS.Data.Model.Entities.Base;
using FreeSql.DataAnnotations;


namespace CMS.Data.Model.Entities
{
    /// <summary>
    /// 配置项
    /// </summary>
    [Table(Name = "cms_settings")]
    public class CMSSetting : BaseEntity<long>
    {
        /// <summary>
        /// 键
        /// </summary>
        [Column(StringLength = 128)]
        public string Name { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [Column(StringLength = 2048)]
        public string Value { get; set; }

        /// <summary>
        /// 提供者
        /// </summary>

        [Column(StringLength = 64)]
        public string ProviderName { get; set; }

        /// <summary>
        /// 提供者值
        /// </summary>
        [Column(StringLength = 64)]
        public string ProviderKey { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()}, Name = {Name}, Value = {Value}, ProviderName = {ProviderName}, ProviderKey = {ProviderKey}";
        }
    }
}
