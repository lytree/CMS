using System;
using CMS.Data.Model.Entities.Base;
using FreeSql.DataAnnotations;


namespace CMS.Data.Model.Entities.Blog
{
    /// <summary>
    ///  频道标签
    /// </summary>
    [Table(Name = "blog_channel_tag")]
    public class ChannelTag : Entity<long>
    {

        public ChannelTag()
        {
        }

        public ChannelTag(long tagId)
        {
            TagId = tagId;
        }

        public ChannelTag(long channelId, long tagId)
        {
            ChannelId = channelId;
            TagId = tagId;
        }

        /// <summary>
        /// 频道Id
        /// </summary>
        public long ChannelId { get; set; }

        /// <summary>
        /// 标签Id
        /// </summary>
        public long TagId { get; set; }

        [Navigate("ChannelId")]
        public virtual Channel Channel { get; set; }
        [Navigate("TagId")]
        public virtual Tag Tag { get; set; }
    }
}
