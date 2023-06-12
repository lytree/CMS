using System;


namespace CMS.Web.Service.Blog.Classifies;

public class ClassifyDto
{
    public string Thumbnail { get; set; }
    public string ThumbnailDisplay { get; set; }
    public int SortCode { get; set; }
    public string ClassifyName { get; set; }
    public int ArticleCount { get; set; } = 0;
    public long? CreateUserId { get; set; }
    public string CreateUserName { get; set; }
    public DateTime CreateTime { get; set; }
}